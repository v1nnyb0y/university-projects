using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Lab._4.Files.parser_zip
{
    class parser
    {
        private string path;

        public parser(string path_to_zip) {
            path = path_to_zip;
        }

        private void de_arch() {
            DirectoryInfo dir = new DirectoryInfo("current_zip");
            dir.Create();
            ZipFile.ExtractToDirectory(path, "current_zip");
        }

        private List<string> return_files() {
            List<string> files = new List<string>();
            DirectoryInfo dir = new DirectoryInfo("current_zip");
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Name.Substring(file.Name.Length - 4, 4) == ".xml")
                {
                    files.Add(@"current_zip\" + file.Name);
                }
            }

            return files;
        }

        private void parser_xml(string path) {
            XmlDocument xml_doc = new XmlDocument();
            xml_doc.Load(path);
            XmlElement xml_root = xml_doc.DocumentElement;

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml_doc.NameTable);
            nsmgr.AddNamespace("ns2", "http://zakupki.gov.ru/oos/export/1");
            nsmgr.AddNamespace("ns", "http://zakupki.gov.ru/oos/types/1");

            XmlNode node = xml_root.SelectSingleNode("ns2:tenderPlan2017/ns:commonInfo/ns:customerInfo", nsmgr);

            XmlNode node_name = node.SelectSingleNode("ns:fullName", nsmgr);
            XmlNode node_INN = node.SelectSingleNode("ns:INN", nsmgr);
            XmlNode node_KPP = node.SelectSingleNode("ns:KPP", nsmgr);

            node = xml_root.SelectSingleNode("ns2:tenderPlan2017/ns:positions", nsmgr);


            int count_pg = 0;
            double sum = 0;
            int count = 0;
            int i = 0;
            if (node != null) {
                XmlNodeList list_pg = node.SelectNodes("*");
                count_pg = list_pg.Count;
                foreach (XmlNode xml_node in list_pg) {
                    XmlNode okpd =
                        xml_node.SelectSingleNode("ns:purchaseObjectInfo/ns:OKPD2Info/ns:OKPD2/ns:code", nsmgr);
                    if (okpd != null) {
                        if (Convert.ToInt32(okpd.InnerText.Substring(0, 2)) == 63) {
                            count++;
                            XmlNode total =
                                xml_node.SelectSingleNode("ns:commonInfo/ns:financeInfo/ns:planPayments/ns:total",
                                    nsmgr);
                            string digit = total.FirstChild.Value;
                            sum += Double.Parse(digit);
                        }
                    }

                    i++;
                }
            }

            bool flag = true;
            double mid = 0;
            if (count != 0)
            {
                mid = sum / count_pg;

            }
            else
            {
                flag = false;
            }

            Console.Write($"Название предприятия: {node_name.InnerText}\n" +
                              $"ИНН: {node_INN.InnerText}\n" +
                              $"КПП: {node_KPP.InnerText}\n" +
                              $"Количество ПГ: {count_pg}\n");
            if (flag)
            {
                Console.WriteLine($"Cр. арифмитическое ПГ с ОКПД больше 63: {mid}\n");
            }
            else
            {
                Console.WriteLine($"Cр. арифмитическое ПГ с ОКПД больше 63 отсутсвует\n");
            }

            Console.ReadKey(true);
        }

        private void remove_files() {
            DirectoryInfo dir = new DirectoryInfo("current_zip");
            if (dir.GetFiles().Length != 0) {
                dir.Delete(true);
            }
        }

        public void start() {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            de_arch();
            List<string> files = return_files();

            foreach (string file in files) {
                parser_xml(file);
            }

            remove_files();
        }
    }
}
