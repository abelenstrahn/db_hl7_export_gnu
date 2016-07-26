using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace EstomedApp
{
    class AppDataUtil
    {
        private static StringArray box(string str)
        {
            StringArray array = new StringArray();
            array.Insert(str);
            return array;
        }

        public static Patients processEstomed(DBUtil.DBResult result)
        {
            Patients Patients = new Patients();
            for(int i=0;i<result.Count;i++)
            {
                List < string > row = result[i];
                Patient Patient = HL7Util.getEmptyPatient();
                HL7Util.setFirstName(ref Patient, row[0]);
                HL7Util.setSecondName(ref Patient, row[1]);
                HL7Util.setFamily(ref Patient, row[2]);
                HL7Util.setBirthDate(ref Patient, row[3].Replace("00:00:00", ""));
                HL7Util.addContact(ref Patient, "email", row[4]);
                HL7Util.addIdentifier(ref Patient, "card", row[5]);
                HL7Util.addIdentifier(ref Patient, "externalCard", row[6]);
                HL7Util.setPatientalCode(ref Patient, row[7]);
                if (row[8].Contains("1"))
                    HL7Util.setGender(ref Patient, "male");
                else
                    HL7Util.setGender(ref Patient, "female");
                HL7Util.addStreetPart(ref Patient, row[9]);
                HL7Util.addStreetPart(ref Patient, row[10]);
                HL7Util.addStreetPart(ref Patient, row[11]);
                HL7Util.setCity(ref Patient, row[12]);
                HL7Util.setPostalCode(ref Patient, row[13]);
                HL7Util.addIdentifier(ref Patient, "emailReceiver", row[14]);
                HL7Util.addIdentifier(ref Patient, "smsReceiver", row[14]);
                HL7Util.addIdentifier(ref Patient, "guardian", row[15]);
                HL7Util.addIdentifier(ref Patient, "patientGuardianId", row[16]);
                String phonePattern = "([^;]+)";
                Regex rgx = new Regex(phonePattern);
                foreach (Match match in rgx.Matches(row[17]))
                {
                    HL7Util.addContact(ref Patient, "phone", match.Groups[1].Value.Replace("(+48)", "").Trim());
                }
                foreach (Match match in rgx.Matches(row[21]))
                {
                    HL7Util.addContact(ref Patient, "phone", match.Groups[1].Value.Replace("(+48)", "").Trim());
                }

                HL7Util.addIdentifier(ref Patient, "TerritorialUnitId", row[18]);
                if(row[19].Contains("1")) {
                    HL7Util.addIdentifier(ref Patient, "IdentityDocumentType", "Dowód osobisty");
                } else if (row[19].Contains("2")) {
                    HL7Util.addIdentifier(ref Patient, "IdentityDocumentType", "Prawo jazdy");
                } else if (row[19].Contains("3")) {
                    HL7Util.addIdentifier(ref Patient, "IdentityDocumentType", "Paszport");
                }
                HL7Util.addIdentifier(ref Patient, "IdentityDocumentNumber", row[20]);
                //HL7Util.addContact(ref Patient, "phone", row[21].Replace("(+48) ",""));
                if(row[22]!="")
                    HL7Util.addPractitioner(ref Patient, row[22], row[23], row[24], row[25], row[31], row[26], row[27], row[28], row[29], row[30]);
                HL7Util.addIdentifier(ref Patient, "InsuranceNo", row[32]);
                HL7Util.addIdentifier(ref Patient, "InsuranceExpireDate", row[33]);
                HL7Util.addIdentifier(ref Patient, "InsuranceType", row[34]);
                HL7Util.addIdentifier(ref Patient, "NfzDepartmentCode", row[35]);
                HL7Util.addIdentifier(ref Patient, "NfzPermissions", row[36]);
                String comapnyNamePattern = "firmy>([^<]+)<";
                Regex comapnyNameRegex = new Regex(comapnyNamePattern);
                foreach (Match match in comapnyNameRegex.Matches(row[37]))
                {
                    HL7Util.addIdentifier(ref Patient, "CompanyName", match.Groups[1].Value.Trim());
                }
                String comapnyNipPattern = "NIP>([^<]+)<";
                Regex comapnyNipRegex = new Regex(comapnyNipPattern);
                foreach (Match match in comapnyNipRegex.Matches(row[37]))
                {
                    HL7Util.addIdentifier(ref Patient, "CompanyNIP", match.Groups[1].Value.Trim());
                }
                Patients.Insert(Patient);
            }
            return Patients;
        }
    }
}