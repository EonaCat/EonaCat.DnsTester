using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using EonaCat.DnsTester.Helpers;

namespace EonaCat.DnsTester
{
    public partial class MainForm : Form
    {
        private bool _useCustomDnsServers;
        private string _dnsServer1, _dnsServer2;
        private DnsRecordType _recordType;
        private int _dnsTotalChecked;

        public bool IsRunning { get; private set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private async void RunTest_Click(object sender, EventArgs e)
        {
            if (!_useCustomDnsServers)
            {
                if (!chkDns1.Checked && !chkDns2.Checked)
                {
                    MessageBox.Show("Please enable DNS 1 or 2 before starting");
                    return;
                }
            }

            if (_useCustomDnsServers && (!chkDns1.Checked && !chkDns2.Checked))
            {
                MessageBox.Show("Please enable DNS 1 or 2 before using custom Dns");
                return;
            }

            var urls = new List<string>();
            SetupView();


            var numThreads = (int)numericUpDown2.Value; // number of concurrent threads to use
            var maxUrls = (int)numericUpDown1.Value; // maximum number of unique URLs to retrieve
            var numUrlsPerThread = maxUrls / numThreads;
            if (numUrlsPerThread == 0)
            {
                numUrlsPerThread = maxUrls;
                numThreads = 1;
            }

            SetSearchEngines();
            urls = await UrlHelper.RetrieveUrlsAsync(numThreads, numUrlsPerThread).ConfigureAwait(false);
            AddUrlToView(urls);

            IsRunning = true;
            await ProcessAsync(_recordType, urls.ToArray(), _dnsServer1, _dnsServer2).ConfigureAwait(false);
            IsRunning = false;
        }

        private void SetSearchEngines()
        {
            UrlHelper.UseSearchEngineYahoo = checkBox1.Checked;
            UrlHelper.UseSearchEngineBing = checkBox2.Checked;
            UrlHelper.UseSearchEngineGoogle = checkBox3.Checked;
            UrlHelper.UseSearchEngineQwant = checkBox8.Checked;
            UrlHelper.UseSearchEngineWolfram = checkBox5.Checked;
            UrlHelper.UseSearchEngineStartPage = checkBox6.Checked;
            UrlHelper.UseSearchEngineYandex = checkBox7.Checked;
        }

        private void SetupView()
        {
            if (_useCustomDnsServers)
            {
                _dnsTotalChecked = 0;
                if (chkDns1.Checked)
                {
                    _dnsTotalChecked++;
                    _dnsServer1 = CustomDns1.Text;
                }

                if (chkDns2.Checked)
                {
                    _dnsTotalChecked++;
                    _dnsServer2 = CustomDns2.Text;
                }
            }
            else
            {
                _dnsTotalChecked = 0;
                if (chkDns1.Checked)
                {
                    _dnsTotalChecked++;
                    _dnsServer1 = dnsList1.SelectedValue.ToString();
                }

                if (chkDns2.Checked)
                {
                    _dnsTotalChecked++;
                    _dnsServer2 = dnsList2.SelectedValue.ToString();
                }

            }

            ResultView.Items.Clear();
            ResultView.Update();
            Application.DoEvents();

            UrlHelper.Log -= UrlHelper_Log;
            UrlHelper.Log += UrlHelper_Log;
        }

        private void UrlHelper_Log(object sender, string e)
        {
            SetStatus(e);
        }

        private void AddUrlToView(List<string> urls)
        {
            ResultView.Invoke(() =>
            {
                foreach (var currentUrl in urls)
                {
                    var listUrl = new ListViewItem(currentUrl);
                    listUrl.SubItems.Add(" ");
                    listUrl.SubItems.Add(" ");
                    listUrl.SubItems.Add(" ");
                    listUrl.SubItems.Add(" ");

                    ResultView.Items.Add(listUrl);
                }

                if (ResultView.Items.Count > 1)
                {
                    ResultView.EnsureVisible(ResultView.Items.Count - 1);
                }

                ResultView.Update();
            });
            Application.DoEvents();
        }


        private void TesterUI_Load(object sender, EventArgs e)
        {
            PopulateDnsLists();
        }

        private void PopulateDnsLists()
        {
            dnsList1.ValueMember = "ip";
            dnsList1.DisplayMember = "name";

            dnsList2.ValueMember = "ip";
            dnsList2.DisplayMember = "name";

            var serverList = Path.Combine(Application.StartupPath, "Servers.xml");
            var servers1 = new DataSet();
            var servers2 = new DataSet();
            servers1.ReadXml(serverList);
            servers2.ReadXml(serverList);

            var dataTable1 = servers1.Tables[0];
            var dataTable2 = servers2.Tables[0];
            dnsList1.DataSource = dataTable1;
            dnsList2.DataSource = dataTable2;
        }


        private void UseCustomServers_CheckedChanged(object sender, EventArgs e)
        {
            _useCustomDnsServers = UseCustomServers.Checked;
            SetUI();
        }

        private void SetUI()
        {
            dnsList1.Enabled = !_useCustomDnsServers && chkDns1.Checked;
            dnsList2.Enabled = !_useCustomDnsServers && chkDns2.Checked;

            CustomDns1.Enabled = _useCustomDnsServers && chkDns1.Checked;
            CustomDns2.Enabled = _useCustomDnsServers && chkDns2.Checked;

            CustomDns1.Visible = _useCustomDnsServers && chkDns1.Checked;
            CustomDns2.Visible = _useCustomDnsServers && chkDns2.Checked;
            lblCustom1.Visible = _useCustomDnsServers && chkDns1.Checked;
            lblCustom2.Visible = _useCustomDnsServers && chkDns2.Checked;
        }


        private async Task ProcessAsync(DnsRecordType recordType, string[] urls, string dnsAddress1, string dnsAddress2)
        {
            if (recordType == 0)
            {
                recordType = DnsRecordType.A;
            }

            var urlsTotal = urls.Length;
            const string dnsId1 = "Dns1";
            const string dnsId2 = "Dns2";

            DnsHelper.OnLog -= DnsHelper_OnLog;
            DnsHelper.OnLog += DnsHelper_OnLog;

            for (var i = 0; i < urlsTotal; i++)
            {
                var currentUrl = urls[i];
                await ExecuteDns1Async(recordType, dnsAddress1, currentUrl, dnsId1, i).ConfigureAwait(false);
                if (chkDns2.Checked)
                {
                    await ExecuteDns2Async(recordType, dnsAddress2, currentUrl, dnsId2, i).ConfigureAwait(false);
                }
                await Task.Delay(100).ConfigureAwait(false);
            }
        }

        private async Task ExecuteDns2Async(DnsRecordType recordType, string dnsAddress2, string currentUrl, string dnsId2,
            int i)
        {
            try
            {
                DnsResponse response2 = null;
                var queryBytes2 = DnsHelper.CreateDnsQueryPacket(currentUrl, recordType);
                response2 = await DnsHelper.SendDnsQueryPacketAsync(dnsId2, dnsAddress2, 53, queryBytes2).ConfigureAwait(false);
                ProcessResponse(response2);
            }
            catch (SocketException socketException)
            {
                SetStatus(
                    Convert.ToString(socketException)!.IndexOf("time", StringComparison.Ordinal) > 0
                        ? $"DNS1 Timeout - No response received for {Convert.ToString(DnsHelper.DnsReceiveTimeout / 1000)} seconds"
                        : Convert.ToString(socketException));
            }
            catch (Exception exception)
            {
                SetStatus(exception.Message);
                i--;
            }
        }

        private async Task ExecuteDns1Async(DnsRecordType recordType, string dnsAddress1, string currentUrl, string dnsId1,
            int i)
        {
            if (chkDns1.Checked)
            {
                try
                {
                    DnsResponse response1 = null;
                    var queryBytes1 = DnsHelper.CreateDnsQueryPacket(currentUrl, recordType);
                    response1 = await DnsHelper.SendDnsQueryPacketAsync(dnsId1, dnsAddress1, 53, queryBytes1).ConfigureAwait(false);
                    ProcessResponse(response1);
                }
                catch (SocketException socketException)
                {
                    SetStatus(
                        Convert.ToString(socketException)!.IndexOf("time", StringComparison.Ordinal) > 0
                            ? $"DNS1 Timeout - No response received for {Convert.ToString(DnsHelper.DnsReceiveTimeout / 1000)} seconds"
                            : Convert.ToString(socketException));
                }
                catch (Exception exception)
                {
                    SetStatus(exception.Message);
                    i--;
                }
            }
        }

        private void DnsHelper_OnLog(object sender, string e)
        {
            SetStatus(e);
        }

        private void ProcessResponse(DnsResponse dnsResponse)
        {
            if (dnsResponse == null || dnsResponse?.Answers == null || !dnsResponse.Answers.Any())
            {
                return;
            }

            // Retrieve stopTime
            var stopTime = DateTime.Now.Ticks;
            var deltaTime = Convert.ToString((double)(stopTime - dnsResponse.StartTime) / 10000000, CultureInfo.InvariantCulture);

            foreach (var answer in dnsResponse.Answers)
            {
                SetStatus(
                    $"ResourceRecord: Name: {answer.Name} : Type : {answer.Type} : Data :  {answer.Data}");
            }

            ResultView.Invoke(() =>
            {
                for (var i = 0; i < ResultView.Items.Count; i++)
                {
                    foreach (var answer in dnsResponse.Answers)
                    {
                        if (ResultView.Items[i].Text != $"{answer.Name.TrimEnd('.')}")
                            continue;

                        string sDeltaTime;
                        switch (dnsResponse.DnsId)
                        {
                            case "Dns1":
                                ResultView.Items[i].SubItems[1].Text =
                                    Convert.ToString(answer.Data) ?? string.Empty;
                                sDeltaTime = Convert.ToString(deltaTime);
                                ResultView.Items[i].SubItems[2].Text =
                                    sDeltaTime.Length > 5 ? sDeltaTime.Substring(0, 5) : sDeltaTime;
                                ResultView.Items[i].ForeColor = System.Drawing.Color.Red;
                                ResultView.EnsureVisible(i);
                                ResultView.Update();
                                Application.DoEvents();
                                ResultView.Items[i].ForeColor = System.Drawing.Color.Black;
                                break;

                            case "Dns2":
                                ResultView.Items[i].SubItems[3].Text =
                                    Convert.ToString(answer.Data) ?? string.Empty;
                                sDeltaTime = Convert.ToString(deltaTime);
                                ResultView.Items[i].SubItems[4].Text =
                                    sDeltaTime.Length > 5 ? sDeltaTime.Substring(0, 5) : sDeltaTime;
                                ResultView.Items[i].ForeColor = System.Drawing.Color.Red;
                                ResultView.EnsureVisible(i);
                                ResultView.Update();
                                Application.DoEvents();
                                ResultView.Items[i].ForeColor = System.Drawing.Color.Black;
                                break;
                        }
                    }
                }
            });
        }

        private async void btnResolveIP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtResolveIP.Text))
            {
                MessageBox.Show("Please enter an IP address to resolve");
                return;
            }

            if (!IPAddress.TryParse(txtResolveIP.Text, out var iPAddress))
            {
                MessageBox.Show("Please enter a valid IP address");
                return;
            }

            await Task.Run(() =>
            {
                try
                {
                    var dnsEntry = Dns.GetHostEntry(iPAddress);
                    txtResolveHost.Invoke(() =>
                    {
                        txtResolveHost.Text = dnsEntry.HostName;
                    });
                }
                catch (Exception)
                {
                    MessageBox.Show($"Could not get hostname for IP address '{txtResolveIP.Text}'");
                }
            }).ConfigureAwait(false);
        }

        private async void btnResolveHost_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtResolveHost.Text))
            {
                MessageBox.Show("Please enter a hostname to resolve");
                return;
            }


            await Task.Run(() =>
            {
                try
                {
                    var dnsEntry = Dns.GetHostEntry(txtResolveHost.Text);

                    txtResolveHost.Invoke(() =>
                    {
                        txtResolveIP.Text = dnsEntry.AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault().Address.ToString();
                    });
                }
                catch (Exception)
                {
                    MessageBox.Show($"Could not get IP address for hostname '{txtResolveHost.Text}'");
                }
            }).ConfigureAwait(false);
        }

        private void SetStatus(string text)
        {
            StatusBox.Invoke(() =>
            {
                StatusBox.Items.Add($"{DateTime.Now}  {text}");
                StatusBox.TopIndex = StatusBox.Items.Count - 1;
                if (StatusBox.Items.Count > STATUS_BAR_SIZE)
                {
                    StatusBox.Items.RemoveAt(0);
                }

                StatusBox.Update();
            });
        }

        const int STATUS_BAR_SIZE = 5000;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Enum.TryParse(comboBox1.SelectedItem.ToString().ToUpper(), out DnsRecordType queryType))
            {
                _recordType = queryType;
            }
        }

        private void chkDns2_CheckedChanged(object sender, EventArgs e)
        {
            SetUI();
        }

        private void chkDns1_CheckedChanged(object sender, EventArgs e)
        {
            SetUI();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            UrlHelper.UseSearchEngineYahoo = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            UrlHelper.UseSearchEngineBing = checkBox2.Checked;
        }
    }
}
