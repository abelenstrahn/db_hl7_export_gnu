using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace EstomedApp
{
    class MainThread : HL7Util.HL7Cb
    {
        private MainThreadCb ui;
        private string host;
        private int port;
        private string dbname;
        private string user;
        private string password;
        private int rowIndex;
        private int rowCount;

        public MainThread(MainThreadCb _ui, string _host, int _port, string _dbname, string _user, string _password)
        {
            ui = _ui;
            host = _host;
            port = _port;
            dbname = _dbname;
            user = _user;
            password = _password;
        }

        public interface MainThreadCb : DBUtil.ConnectCb, HL7Util.HL7Cb, NetUtil.ScanPortsCb
        {
            void onError(string msg);
        }

        public void processEstomed()
        {
            DBUtil.MysqlDBConnection db = DBUtil.MysqlDBConnection.Instance();
            if(db.connect(ui, host, port, dbname, user, password))
            {
                int loop = 1;
                string tmpFile = System.IO.Path.GetTempPath().ToString() + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "hl7dbexport.hl7";
                DBUtil.DBResult countResult = db.query("Select count(*) from patient");
                rowCount = (Int32.Parse(countResult[0][0])/1000)+1;
                while (true)
                {
                    String q = "Select  p.FirstName, p.SecondName, p.LastName, p.BirthDate, p.Email, p.CardNo, p.ExternalCardNo, p.PeselNo, p.Sex, p.AddressPart1, p.AddressPart2, p.AddressPart3, p.City, p.ZipCode, p.AgreesForEmailVisitNotifications, p.Guardian, p.PatientGuardianId, p.NormalizedPhoneNumber, p.TerritorialUnitId, p.IdentityDocumentType, p.IdentityDocumentNumber, p.ContactInfo, g.FirstName, g.LastName, g.PeselNo, g.PhoneNo, g.City, g.ZipCode, g.Street, g.StreetNo, g.FlatNo, g.RelationType, p.InsuranceNo, p.InsuranceExpireDate, p.InsuranceType, nfzc.NfzDepartmentCode, nfzd.Permissions, p.CompanyInfoXml from patient p left join patientguardianpatient pgp on p.Id = pgp.PatientId left join patientguardian g on pgp.PatientGuardianId = g.Id left join nfzdata nfzd on nfzd.PatientId = p.Id left join nfzcode nfzc on nfzd.NfzCodeID = nfzc.Id limit " + (loop * 1000).ToString() + " offset " + ((loop - 1) * 1000).ToString();
                    DBUtil.DBResult result = db.query(q);
                    Patients patients = AppDataUtil.processEstomed(result);
                    if (patients.Size() == 0)
                    {
                        if(loop==1)
                        {
                            //empty?? wtf
                            ui.onError("Brak danych w bazie");
                        } else
                        {
                            ui.onHL7Done(tmpFile);
                        }
                        db.Close();
                        return;
                    }
                    string stream = "";
                    HL7Util.processPatientsPart(this, ref stream, patients, tmpFile, loop==1, loop==rowCount);
                    loop++;
                }
            }
        }

        public void onHL7Progress(double progress)
        {
                double res = progress;
                res = 100 * ((res / 100) + rowIndex) / rowCount;
                ui.onHL7Progress(res);
        }

        public void onHL7Done(string tmpFile)
        {
            rowIndex++;
        }
    }
}
