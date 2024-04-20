using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using CoreScanner;
using System.Globalization;

namespace Scanner_SDK_Sample_Application
{
    public partial class frmScannerApp
    {
        /// <summary>
        /// Flush Macro PDF
        /// </summary>
        private string tempbarcode = "";
        private void PerformBtnFlushMacroPdfClick(object sender, EventArgs e)
        {
            if (IsScannerConnected())
            {
                string inXml = GetScannerIDXml(), outXml = String.Empty;
                int iOpCode = FLUSH_MACROPDF, iStatus = STATUS_FALSE;
                
                ExecCmd(iOpCode, ref inXml, out outXml, out iStatus);
                DisplayResult(iStatus, "FLUSH_MACROPDF");
            }
        }

        /// <summary>
        /// Abort Macro PDF
        /// </summary>
        private void PerformBtnAbortMacroPdfClick(object sender, EventArgs e)
        {
            if (IsScannerConnected())
            {
                string inXml = GetScannerIDXml(), outXml = String.Empty;
                int iOpCode = ABORT_MACROPDF, iStatus = STATUS_FALSE;
                
                ExecCmd(iOpCode, ref inXml, out outXml, out iStatus);
                DisplayResult(iStatus, "ABORT_MACROPDF");
            }
        }

        /// <summary>
        /// Clear barcode event data populations
        /// </summary>
        private void PerformClearBarcodeDataClick(object sender, EventArgs e)
        {
            txtBarcode.Clear();
            txtBarcodeLbl.Clear();
            txtSyblogy.Clear();
        }

        /// <summary>
        /// Barcode symbology
        /// </summary>
        /// <param name="Code">Symbology code</param>
        /// <returns>Symbology name</returns>
        private string GetSymbology(int Code)
        {
            switch (Code)
            {
                case ST_NOT_APP:
                    return "NOT APPLICABLE";
                case ST_CODE_39:
                    return "CODE 39";
                case ST_CODABAR:
                    return "CODABAR";
                case ST_CODE_128:
                    return "CODE 128";
                case ST_D2OF5:
                    return "DISCRETE 2 OF 5";
                case ST_IATA:
                    return "IATA";
                case ST_I2OF5:
                    return "INTERLEAVED 2 OF 5";
                case ST_CODE93:
                    return "CODE 93";
                case ST_UPCA:
                    return "UPC-A";
                case ST_UPCE0:
                    return "UPC-E0";
                case ST_EAN8:
                    return "EAN-8";
                case ST_EAN13:
                    return "EAN-13";
                case ST_CODE11:
                    return "CODE 11";
                case ST_CODE49:
                    return "CODE 49";
                case ST_MSI:
                    return "MSI";
                case ST_EAN128:
                    return "EAN-128";
                case ST_UPCE1:
                    return "UPC-E1";
                case ST_PDF417:
                    return "PDF-417";
                case ST_CODE16K:
                    return "CODE 16K";
                case ST_C39FULL:
                    return "CODE 39 FULL ASCII";
                case ST_UPCD:
                    return "UPC-D";
                case ST_TRIOPTIC:
                    return "CODE 39 TRIOPTIC";
                case ST_BOOKLAND:
                    return "BOOKLAND";
                case ST_UPCA_W_CODE128:
                    return "UPC-A w/Code 128 Supplemental";
                case ST_JAN13_W_CODE128:
                    return "EAN/JAN-13 w/Code 128 Supplemental";
                case ST_NW7:
                    return "NW-7";
                case ST_ISBT128:
                    return "ISBT-128";
                case ST_MICRO_PDF:
                    return "MICRO PDF";
                case ST_DATAMATRIX:
                    return "DATAMATRIX";
                case ST_QR_CODE:
                    return "QR CODE";
                case ST_MICRO_PDF_CCA:
                    return "MICRO PDF CCA";
                case ST_POSTNET_US:
                    return "POSTNET US";
                case ST_PLANET_CODE:
                    return "PLANET CODE";
                case ST_CODE_32:
                    return "CODE 32";
                case ST_ISBT128_CON:
                    return "ISBT-128 CON";
                case ST_JAPAN_POSTAL:
                    return "JAPAN POSTAL";
                case ST_AUS_POSTAL:
                    return "AUS POSTAL";
                case ST_DUTCH_POSTAL:
                    return "DUTCH POSTAL";
                case ST_MAXICODE:
                    return "MAXICODE";
                case ST_CANADIN_POSTAL:
                    return "CANADIAN POSTAL";
                case ST_UK_POSTAL:
                    return "UK POSTAL";
                case ST_MACRO_PDF:
                    return "MACRO PDF";
                case ST_MACRO_QR_CODE:
                    return "MACRO QR CODE";
                case ST_MICRO_QR_CODE:
                    return "MICRO QR CODE";
                case ST_AZTEC:
                    return "AZTEC";
                case ST_AZTEC_RUNE:
                    return "AZTEC RUNE";
                case ST_DISTANCE:
                    return "DISTANCE";
                case ST_GS1_DATABAR:
                    return "GS1 DATABAR";
                case ST_GS1_DATABAR_LIMITED:
                    return "GS1 DATABAR LIMITED";
                case ST_GS1_DATABAR_EXPANDED:
                    return "GS1 DATABAR EXPANDED";
                case ST_PARAMETER:
                    return "PARAMETER";
                case ST_USPS_4CB:
                    return "USPS 4CB";
                case ST_UPU_FICS_POSTAL:
                    return "UPU FICS POSTAL";
                case ST_ISSN:
                    return "ISSN";
                case ST_SCANLET:
                    return "SCANLET";
                case ST_CUECODE:
                    return "CUECODE";
                case ST_MATRIX2OF5:
                    return "MATRIX 2 OF 5";
                case ST_UPCA_2:
                    return "UPC-A + 2 SUPPLEMENTAL";
                case ST_UPCE0_2:
                    return "UPC-E0 + 2 SUPPLEMENTAL";
                case ST_EAN8_2:
                    return "EAN-8 + 2 SUPPLEMENTAL";
                case ST_EAN13_2:
                    return "EAN-13 + 2 SUPPLEMENTAL";
                case ST_UPCE1_2:
                    return "UPC-E1 + 2 SUPPLEMENTAL";
                case ST_CCA_EAN128:
                    return "CCA EAN-128";
                case ST_CCA_EAN13:
                    return "CCA EAN-13";
                case ST_CCA_EAN8:
                    return "CCA EAN-8";
                case ST_CCA_RSS_EXPANDED:
                    return "GS1 DATABAR EXPANDED COMPOSITE (CCA)";
                case ST_CCA_RSS_LIMITED:
                    return "GS1 DATABAR LIMITED COMPOSITE (CCA)";
                case ST_CCA_RSS14:
                    return "GS1 DATABAR COMPOSITE (CCA)";
                case ST_CCA_UPCA:
                    return "CCA UPC-A";
                case ST_CCA_UPCE:
                    return "CCA UPC-E";
                case ST_CCC_EAN128:
                    return "CCA EAN-128";
                case ST_TLC39:
                    return "TLC-39";
                case ST_CCB_EAN128:
                    return "CCB EAN-128";
                case ST_CCB_EAN13:
                    return "CCB EAN-13";
                case ST_CCB_EAN8:
                    return "CCB EAN-8";
                case ST_CCB_RSS_EXPANDED:
                    return "GS1 DATABAR EXPANDED COMPOSITE (CCB)";
                case ST_CCB_RSS_LIMITED:
                    return "GS1 DATABAR LIMITED COMPOSITE (CCB)";
                case ST_CCB_RSS14:
                    return "GS1 DATABAR COMPOSITE (CCB)";
                case ST_CCB_UPCA:
                    return "CCB UPC-A";
                case ST_CCB_UPCE:
                    return "CCB UPC-E";
                case ST_SIGNATURE_CAPTURE:
                    return "SIGNATURE CAPTUREE";
                case ST_MOA:
                    return "MOA";
                case ST_PDF417_PARAMETER:
                    return "PDF417 PARAMETER";
                case ST_CHINESE2OF5:
                    return "CHINESE 2 OF 5";
                case ST_KOREAN_3_OF_5:
                    return "KOREAN 3 OF 5";
                case ST_DATAMATRIX_PARAM:
                    return "DATAMATRIX PARAM";
                case ST_CODE_Z:
                    return "CODE Z";
                case ST_UPCA_5:
                    return "UPC-A + 5 SUPPLEMENTAL";
                case ST_UPCE0_5:
                    return "UPC-E0 + 5 SUPPLEMENTAL";
                case ST_EAN8_5:
                    return "EAN-8 + 5 SUPPLEMENTAL";
                case ST_EAN13_5:
                    return "EAN-13 + 5 SUPPLEMENTAL";
                case ST_UPCE1_5:
                    return "UPC-E1 + 5 SUPPLEMENTAL";
                case ST_MACRO_MICRO_PDF:
                    return "MACRO MICRO PDF";
                case ST_OCRB:
                    return "OCRB";
                case ST_OCRA:
                    return "OCRA";
                case ST_PARSED_DRIVER_LICENSE:
                    return "PARSED DRIVER LICENSE";
                case ST_PARSED_UID:
                    return "PARSED UID";
                case ST_PARSED_NDC:
                    return "PARSED NDC";
                case ST_DATABAR_COUPON:
                    return "DATABAR COUPON";
                case ST_PARSED_XML:
                    return "PARSED XML";
                case ST_HAN_XIN_CODE:
                    return "HAN XIN CODE";
                case ST_CALIBRATION:
                    return "CALIBRATION";
                case ST_GS1_DATAMATRIX:
                    return "GS1 DATA MATRIX";
                case ST_GS1_QR:
                    return "GS1 QR";
                case BT_MAINMARK:
                    return "MAIL MARK";
                case BT_DOTCODE:
                    return "DOT CODE";
                case BT_GRID_MATRIX:
                    return "GRID MATRIX";
                case ST_EPC_RAW:
                    return "EPC RAW";
                case BT_UDI_CODE:
                    return "UDI CODE";
                default:
                    return "";
            }
            
        }

        /// <summary>
        /// Populate Barcode data controls
        /// </summary>
        /// <param name="strXml">Barcode data XML</param>
        private void ShowBarcodeLabel(string strXml)
        {
            System.Diagnostics.Debug.WriteLine("Initial XML" + strXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strXml);

            string strData = String.Empty;
            string barcode = xmlDoc.DocumentElement.GetElementsByTagName("datalabel").Item(0).InnerText;
            string symbology = xmlDoc.DocumentElement.GetElementsByTagName("datatype").Item(0).InnerText;
            string[] numbers = barcode.Split(' ');

            foreach (string number in numbers)
            {
                if (String.IsNullOrEmpty(number))
                {
                    break;
                }

                strData += ((char)Convert.ToInt32(number, 16)).ToString();
            }

            /**if (txtBarcodeLbl.InvokeRequired)
            {
                txtBarcodeLbl.Invoke(new MethodInvoker(delegate
                {
                    txtBarcodeLbl.Clear();
                    System.Diagnostics.Debug.WriteLine(strData);
                    txtBarcodeLbl.Text = PNRExtract(strData);
                  

                }));
            }**/

            System.Diagnostics.Debug.WriteLine(GetSymbology((int)Convert.ToInt32(symbology)));
            if (txtSyblogy.InvokeRequired)
            {
                txtSyblogy.Invoke(new MethodInvoker(delegate
                {
                    txtSyblogy.Text = GetSymbology((int)Convert.ToInt32(symbology));
                   
                }));
            }
            if (GetSymbology((int)Convert.ToInt32(symbology)) == "PDF-417")
            {
                tempbarcode = strData;
                if (txtPNRLbl.InvokeRequired)
                {
                    txtPNRLbl.Invoke(new MethodInvoker(delegate
                    {
                        txtPNRLbl.Clear();
                        System.Diagnostics.Debug.WriteLine(strData);
                        txtPNRLbl.Text = PNRExtract(strData);


                    }));
                }
                if (txtFLIGHTLbl.InvokeRequired)
                {
                    txtFLIGHTLbl.Invoke(new MethodInvoker(delegate
                    {
                        txtFLIGHTLbl.Clear();
                        System.Diagnostics.Debug.WriteLine(strData);
                        txtFLIGHTLbl.Text = FlightExtract(strData);


                    }));
                }
            }
        }

        private void SetWriteStatusIcon(CommandStatus status)
        {
            Bitmap icon = null;
            switch (status)
            {
                case CommandStatus.Failed:
                    icon = Properties.Resources.icon_error;
                    break;
                case CommandStatus.Success:
                    icon = Properties.Resources.icon_success;
                    break;
            }

            if (writeStatusIcon.InvokeRequired)
            {
                writeStatusIcon.Invoke(new MethodInvoker(delegate
                {
                    if (icon == null)
                    {
                        writeStatusIcon.Visible = false;
                    }
                    else
                    {
                        writeStatusIcon.Visible = true;
                        writeStatusIcon.Image = icon;
                    }
                }));
            }
            else
            {
                if (icon == null)
                {
                    writeStatusIcon.Visible = false;
                }
                else
                {
                    writeStatusIcon.Visible = true;
                    writeStatusIcon.Image = icon;
                }
            }
        }
        /// <summary>
        /// BarcodeEvent received
        /// </summary>
        /// <param name="eventType">Type of event</param>
        /// <param name="scanData">Barcode string</param>
        void OnBarcodeEvent(short eventType, ref string scanData)
        {
            try
            {
                string tmpScanData = scanData;

                UpdateResults("Barcode Event fired");
                ShowBarcodeLabel(tmpScanData);

                System.Diagnostics.Debug.WriteLine(IndentXmlString(tmpScanData));

                if (txtBarcode.InvokeRequired)
                {
                    txtBarcode.Invoke(new MethodInvoker(delegate
                    {
                        txtBarcode.Text = IndentXmlString(tmpScanData);
               
                     
                        System.Diagnostics.Debug.WriteLine(IndentXmlString(tmpScanData));
                      
                    }));
                }
                if (GetScanDataType(tmpScanData) == ST_EPC_RAW)
                {
                  
                    currentEpcId = GetScanDataLabel(tmpScanData);
                    currentEpcId = GetReadableScanDataLabel(currentEpcId);
                    
                    string newepc = StringToHex(txtPNRLbl.Text + txtFLIGHTLbl.Text);
                         
                    System.Diagnostics.Debug.WriteLine(currentEpcId);
                    System.Diagnostics.Debug.WriteLine(newepc);
                    var status = WriteTag(currentEpcId, RfidBank.Epc, newepc, 2, "00");
                    if (status == STATUS_SUCCESS)
                    {
                        System.Diagnostics.Debug.WriteLine("WRITE SUCCESS");
                        SetWriteStatusIcon(CommandStatus.Success);
                    }
                    else
                    {
                        SetWriteStatusIcon(CommandStatus.Failed);
                        System.Diagnostics.Debug.WriteLine("WRITE FAIL");
                    }
                    //SetTextboxText(txtEpcId, currentEpcId);

                    ExtractEpcData();
                }

                if (GetSelectedTabName().Equals(SSW_TAB_NAME))
                {
                    if (GetScanDataType(tmpScanData) == ST_UPCA)
                    {
                        currentUpca = GetScanDataLabel(tmpScanData);
                        currentUpca = GetReadableScanDataLabel(currentUpca);
                        SetTextboxText(txtUpcaBarcode, currentUpca);

                        ExtractUpcData();
                    }
                    if(GetScanDataType(tmpScanData) == ST_EPC_RAW) 
                    {
                        currentEpcId = GetScanDataLabel(tmpScanData);
                        currentEpcId = GetReadableScanDataLabel(currentEpcId);

                        SetTextboxText(txtEpcId, currentEpcId);

                        ExtractEpcData();
                    }                   
                    CreateNewEpcId();
                }
            }
            catch (Exception e)
            {
                UpdateResults("Failed to scan data  "+e.Message);
            }
        }
        private static string BoardingPassMassager(string s)
        {
            System.Diagnostics.Debug.WriteLine(s);
            string ss_name = s.Substring(2, 19);
            System.Diagnostics.Debug.WriteLine(ss_name);
            string ss_pnr = s.Substring(23, 6);
            System.Diagnostics.Debug.WriteLine(ss_pnr);
            string ss_flight = s.Substring(30, 8);
            System.Diagnostics.Debug.WriteLine(ss_flight);
             string ss_no = s.Substring(39, 4);
            //  string ss_date = s.Substring(44, 12);
            // string date = null;
            // int currentYear = DateTime.Now.Year;
            // date = DecodeJulianDate(currentYear, int.Parse(ss_date.Substring(0, 3)));
            // string seats = ss_date.Substring(4, 4);
            //string seq = ss_date.Substring(8, 4);
            //string[] name_part = ss_name.Split('/');
            // string origin = ss_flight.Substring(0, 3);
            // string dest = ss_flight.Substring(3, 3);
            string flight_no = ss_flight.Substring(ss_flight.Length - 2) + ss_no;
            System.Diagnostics.Debug.WriteLine(flight_no);
            string result = StringToHex(ss_pnr + "" + flight_no);
            System.Diagnostics.Debug.WriteLine(result);
            return result;
        }
        private static string PNRExtract(string s)
        {

            string ss_pnr = s.Substring(23, 6);
            System.Diagnostics.Debug.WriteLine(ss_pnr);
    
            return ss_pnr;
        }
        private static string FlightExtract(string s)
        {
       
            string ss_flight = s.Substring(30, 8);
            System.Diagnostics.Debug.WriteLine(ss_flight);
            string ss_no = s.Substring(39, 4);
            //  string ss_date = s.Substring(44, 12);
            // string date = null;
            // int currentYear = DateTime.Now.Year;
            // date = DecodeJulianDate(currentYear, int.Parse(ss_date.Substring(0, 3)));
            // string seats = ss_date.Substring(4, 4);
            //string seq = ss_date.Substring(8, 4);
            //string[] name_part = ss_name.Split('/');
            // string origin = ss_flight.Substring(0, 3);
            // string dest = ss_flight.Substring(3, 3);
            string flight_no = ss_flight.Substring(ss_flight.Length - 2) + ss_no;
            System.Diagnostics.Debug.WriteLine(flight_no);

            return flight_no;
        }
        public static string StringToHex(string input)
        {
            // Get byte array of the string
            byte[] bytes = Encoding.UTF8.GetBytes(input);

            // Initialize StringBuilder to store hexadecimal representation
            StringBuilder hexString = new StringBuilder();

            // Iterate over each byte and convert to hexadecimal
            foreach (byte b in bytes)
            {
                // Convert byte to hexadecimal string
                string hex = b.ToString("X2"); // X2 format specifier ensures two characters for each byte
                                               // Append hexadecimal string to result
                hexString.Append(hex);
            }

            // Return the hexadecimal representation
            return hexString.ToString();
        }
        private static string DecodeJulianDate(int year, int dayOfYear)
        {
            // Construct DateTime object for January 1st of the specified year
            DateTime january1st = new DateTime(year, 1, 1);
            january1st = january1st.AddDays(dayOfYear - 1); // Subtract 1 because day of year is 1-indexed
            string formattedDate = january1st.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture); // Example: "dd-MM-yyyy" for day-month-year format
            return formattedDate;
        }
        /// <summary>
        /// Get the selected tab name
        /// </summary>
        /// <returns>Name of the selected tab</returns>
        private string GetSelectedTabName()
        {
            string selectedTabName = String.Empty;
            if (tabCtrl.InvokeRequired)
            {
                tabCtrl.Invoke(new MethodInvoker(delegate
                {
                    selectedTabName = tabCtrl.SelectedTab.Name;
                }));
            }
            else
            {
                selectedTabName = tabCtrl.SelectedTab.Name;
            }
            return selectedTabName;
        }

        /// <summary>
        /// Get a readable string form the hex string
        /// </summary>
        /// <param name="scanDataLabel">Hex formated string</param>
        /// <returns>Readable string</returns>
        private string GetReadableScanDataLabel(string scanDataLabel)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string[] numbers = scanDataLabel.Split(' ');
        
            foreach (string number in numbers)
            {
                if (String.IsNullOrEmpty(number))
                {
                    break;
                }
                int character = Convert.ToInt32(number, 16);
                stringBuilder.Append(((char)character).ToString());
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Get data label from the CoreScanner's barcode event XML
        /// </summary>
        /// <param name="scanDataXml">CoreScanner's barcode event XML</param>
        /// <returns>Content of datalabel tag</returns>
        private string GetScanDataLabel(string scanDataXml)
        {
            try
            {
               
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(scanDataXml);
                return xmlDocument.DocumentElement.GetElementsByTagName("datalabel").Item(0).InnerText;
            }
            catch
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Get the data type from CoreScanner's barcode event XML
        /// </summary>
        /// <param name="scanDataXml">CoreScanner's barcode event XML</param>
        /// <returns>Content of datatype tag</returns>
        private int GetScanDataType(string scanDataXml)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(scanDataXml);
                return (int)Convert.ToInt32(xmlDocument.DocumentElement.GetElementsByTagName("datatype").Item(0).InnerText.Trim());
            }
            catch
            {
                return ST_NOT_APP;
            }
            
        }

        private string m_DadfSource = "";
        private string m_DadfPath = "";

        private void ResetDADF()
        {
            int opcode = RESET_DADF;
            string inXML = "<inArgs></inArgs>";
            string outXml = "";
            int status = STATUS_FALSE;
            ExecCmd(opcode, ref inXML, out outXml, out status);
            DisplayResult(status, "RESET_DADF");
        }

        private void SetDADF()
        {
            int opcode = CONFIGURE_DADF;
            string inXML = "<inArgs><cmdArgs><arg-string>" + m_DadfPath + "</arg-string></cmdArgs></inArgs>";
            string outXml = "";
            int status = STATUS_FALSE;
            ExecCmd(opcode, ref inXML, out outXml, out status);
            DisplayResult(status, "CONFIGURE_DADF");
        }

        private void OnChkChangedDADF(object sender, EventArgs e)
        {
            if(chkBoxAppADF.CheckState == CheckState.Unchecked)
	        {
		        ResetDADF();
                chkBoxAppADF.Text = "Not Set";
                chkBoxAppADF.Enabled = false;
                m_DadfSource = "";
	        }
        }

        private void PerformBtnBrowseScriptClick(object sender, EventArgs e)
        {
            if (openFileDialogDADF.ShowDialog() == DialogResult.OK)
            {
                m_DadfPath = openFileDialogDADF.FileName;
                if (m_DadfPath == "") return;

                SetDADF();

                chkBoxAppADF.Enabled = true;
                chkBoxAppADF.Checked = true;
                chkBoxAppADF.Text = "Unload";
                m_DadfSource = "";
            }
        }

        private void PerformBtnScriptEditorClick(object sender, EventArgs e)
        {
            DadfScriptEditor frm = new DadfScriptEditor();
            frm.ScriptSource = m_DadfSource;

            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                m_DadfSource = frm.ScriptSource;
                if (m_DadfSource == "")
                {
                    chkBoxAppADF.Checked = false;
                    chkBoxAppADF.Text = "Not Set";
                    chkBoxAppADF.Enabled = false;

                    ResetDADF();

                    m_DadfPath = "";
                    m_DadfSource = "";
                }
                else
                {
                    chkBoxAppADF.Enabled = true;
                    chkBoxAppADF.Checked = true;
                    chkBoxAppADF.Text = "Unload";

                    //XML encode entities
                    string scriptSource = m_DadfSource;
                    scriptSource = scriptSource.Replace("&", "&amp;");
                    scriptSource = scriptSource.Replace("<", "&lt;");
                    scriptSource = scriptSource.Replace(">", "&gt;");
                    scriptSource = scriptSource.Replace("\'", "&apos;");
                    scriptSource = scriptSource.Replace("\"", "&quot;");
                    //m_DadfSource = scriptSource;
                    m_DadfPath = scriptSource;
                    SetDADF();
                    m_DadfPath = "";
                }
            }
        }

    }
}
