using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace EstomedApp
{
    class HL7Util
    {
        const int indent = 4;

        public interface HL7Cb
        {
            void onHL7Progress(double progress);
            void onHL7Done(string tmpFile);
        }

        private static void al(ref string stream, string line, int ix)
        {
            for (int i = 0; i < ix; i++)
                stream += " ";
            stream += line + "\n";
        }

        private static void fflush(ref string stream, string file)
        {
            while(true)
            try
            {
                using (StreamWriter writer = File.AppendText(file))
                {
                    writer.WriteLine(stream);
                }
                stream = "";
                return;
            }
            catch (Exception e)
            {
                Thread.Sleep(1);
            }
        }

        private static void a(ref string stream, string line, int ix=0)
        {
            for (int i = 0; i < ix; i++)
                stream += " ";
            stream += line;
        }

        public static bool prepareStringArrayObj(ref string ret, string typeName, StringArray array, int ix)
        {
            bool empty = true;
            for (int i = 0; i < array.Size(); i++)
            {
                if (array[i].Length > 0)
                {
                    empty = false;
                    al(ref ret, String.Format("<{0} value=\"{1}\" />", typeName, array[i]), ix);
                }
            }
            return empty;
        }

        public static void prepareSystems(ref string ret, StringArray systems, int ix)
        {
            prepareStringArrayObj(ref ret, "system", systems, ix);
        }

        public static void prepareUses(ref string ret, StringArray uses, int ix)
        {
            prepareStringArrayObj(ref ret, "use", uses, ix);
        }

        public static void prepareTypes(ref string ret, StringArray types, int ix)
        {
            prepareStringArrayObj(ref ret, "type", types, ix);
        }

        public static void prepareDiscricts(ref string ret, StringArray districts, int ix)
        {
            prepareStringArrayObj(ref ret, "district", districts, ix);
        }

        public static void prepareCountries(ref string ret, StringArray countries, int ix)
        {
            prepareStringArrayObj(ref ret, "country", countries, ix);
        }

        public static void prepareCities(ref string ret, StringArray cities, int ix)
        {
            prepareStringArrayObj(ref ret, "city", cities, ix);
        }

        public static void prepareLines(ref string ret, StringArray lines, int ix)
        {
            prepareStringArrayObj(ref ret, "line", lines, ix);
        }

        public static void prepareStates(ref string ret, StringArray states, int ix)
        {
            prepareStringArrayObj(ref ret, "state", states, ix);
        }

        public static void preparePostalCode(ref string ret, StringArray postalCodes, int ix)
        {
            prepareStringArrayObj(ref ret, "postalCode", postalCodes, ix);
        }

        public static void prepareFamilies(ref string ret, StringArray families, int ix)
        {
            prepareStringArrayObj(ref ret, "family", families, ix);
        }

        public static void prepareGivens(ref string ret, StringArray givens, int ix)
        {
            prepareStringArrayObj(ref ret, "given", givens, ix);
        }
        public static void preparePrefixes(ref string ret, StringArray prefixes, int ix)
        {
            prepareStringArrayObj(ref ret, "prefix", prefixes, ix);
        }

        public static void prepareSufixes(ref string ret, StringArray sufixes, int ix)
        {
            prepareStringArrayObj(ref ret, "sufix", sufixes, ix);
        }

        public static void prepareValues(ref string ret, StringArray values, int ix)
        {
            prepareStringArrayObj(ref ret, "value", values, ix);
        }

        public static void prepareTexts(ref string ret, StringArray texts, int ix)
        {
            prepareStringArrayObj(ref ret, "text", texts, ix);
        }

        public static void preparePeroids(ref string ret, Periods peroids, int ix)
        {
            for (int i = 0; i < peroids.Size(); i++)
            {
                Period peroid = peroids[i];
                if (peroid.getStart().Size() + peroid.getEnd().Size() == 0)
                    return;
                al(ref ret, "<peroid>", ix);
                if (peroid.getStart().Size() > 0)
                    al(ref ret, String.Format("<start value = \"{0}\" />", peroid.getStart()[0]), ix + indent);
                if (peroid.getStart().Size() > 0)
                    al(ref ret, String.Format("<end value = \"{0}\" />", peroid.getEnd()[0]), ix + indent);
                al(ref ret, "</peroid>", ix);
            }
            
        }

        public static void prepareAssigners(ref string ret, StringArray assigners, int ix)
        {
            for (int i = 0; i < assigners.Size(); i++)
            {
                al(ref ret, "<assigner>", ix);
                al(ref ret, String.Format("   <reference value=\"{0}\"", assigners[i]), ix+indent);
                al(ref ret, "</assigner>", ix);
            }
        }

        public static void processIdentifier(ref string ret, Identifier indentifier, int ix)
        {
            string values = "";
            prepareValues(ref values, indentifier.getValue(), ix + indent);
            if (values.Length > 0) {
              al(ref ret, "<identifier>", ix);
              prepareSystems(ref ret, indentifier.getSystem(), ix+indent);
              a(ref ret, values);
              preparePeroids(ref ret, indentifier.getPeriod(), ix + indent);
              prepareAssigners(ref ret, indentifier.getAssigner(), ix + indent);
              prepareUses(ref ret, indentifier.getUse(), ix + indent);
              al(ref ret, "</identifier>", ix);
            }
        }

        public static void processCodeableConcept(ref string ret, CodeableConcept codeableConcept, int ix)
        {
            prepareTexts(ref ret, codeableConcept.getText(), ix);
        }

        public static void processPractitionerRole(ref string ret, PractitionerRole practitionerRole, int ix)
        {
            al(ref ret, "<practitionerRole>", ix);
            processCodeableConcept(ref ret, practitionerRole.getRole(), ix+indent);
            al(ref ret, "</practitionerRole>", ix);
        }

        public static void processPractitionerRoles(ref string ret, PractitionerRoles practitionerRoles, int ix)
        {
            for (int i = 0; i < practitionerRoles.Size(); i++)
                processPractitionerRole(ref ret, practitionerRoles[i], ix);
        }

        public static void processPractitioner(ref string ret, Practitioner practitioner, int ix)
        {
            al(ref ret, "<Practitioner>", ix);
            for (int i = 0; i < practitioner.getIdentifier().Size(); i++)
            {
                processIdentifier(ref ret, practitioner.getIdentifier()[i], ix+indent);
            }
            for (int i = 0; i < practitioner.getName().Size(); i++)
            {
                processHumanName(ref ret, practitioner.getName()[i], ix + indent);
            }
            for (int i = 0; i < practitioner.getTelecom().Size(); i++)
            {
                processTelecom(ref ret, practitioner.getTelecom()[i], ix + indent);
            }
            processGender(ref ret, practitioner.getGender(), ix + indent);
            processBirthDate(ref ret, practitioner.getBirthData(), ix + indent);
            for (int i = 0; i < practitioner.getAddress().Size(); i++)
            {
                processAdress(ref ret, practitioner.getAddress()[i], ix + indent);
            }
            processPractitionerRoles(ref ret, practitioner.getPractitionerRole(), ix+indent);
            al(ref ret, "</Practitioner>", ix);
        }

        public static void processCareProviders(ref string ret, CareProviders providers, int ix)
        {
            for (int i = 0; i < providers.Size(); i++)
            {
                al(ref ret, "<careProvider>", ix);
                if (providers[i].isPractitioner())
                    processPractitioner(ref ret, providers[i].asPractitioner(), ix+indent);
                //else if(providers[i].isOrganization())
                //    processOrganization(ref ret, providers[i]);
                al(ref ret, "</careProvider>", ix);
            }
        }
        public static void processHumanName(ref string ret, HumanName humanName, int ix)
        {
            al(ref ret, "<name>", ix);
            prepareUses(ref ret, humanName.getUse(), ix+indent);
            prepareTexts(ref ret, humanName.getText(), ix + indent);
            prepareFamilies(ref ret, humanName.getFamily(), ix + indent);
            prepareGivens(ref ret, humanName.getGiven(), ix + indent);
            preparePrefixes(ref ret, humanName.getPrefix(), ix + indent);
            prepareSufixes(ref ret, humanName.getSuffix(), ix + indent);
            preparePeroids(ref ret, humanName.getPeriod(), ix + indent);
            al(ref ret, "</name>", ix);
        }

        public static void processTelecom(ref string ret, ContactPoint contact, int ix)
        {
            string ins = "";
            prepareValues(ref ins, contact.getValue(), ix + indent);
            if(ins.Length > 0)
            {
                al(ref ret, "<telecom>", ix);
                prepareSystems(ref ret, contact.getSystem(), ix + indent);
                prepareUses(ref ret, contact.getUse(), ix + indent);
                a(ref ret, ins);
                preparePeroids(ref ret, contact.getPeriod(), ix + indent);
                al(ref ret, "</telecom>", ix);
            }
           
        }

        public static void processGender(ref string ret, StringArray gender, int ix)
        {
            if(gender.Size()>0)
            {
                al(ref ret, String.Format("<gender value=\"{0}\"/>", gender[0]), ix);
            }
        }

        public static void processBirthDate(ref string ret, StringArray birthDate, int ix)
        {
            if (birthDate.Size() > 0)
            {
                al(ref ret, String.Format("<birthDate value=\"{0}\"/>", birthDate[0]), ix);
            }
        }

        public static void processAdress(ref string ret, Address address, int ix)
        {
            al(ref ret, "<address>", ix);
            prepareUses(ref ret, address.getUse(), ix+indent);
            //prepareTypes(ref ret, address.GetType(), ix+indent);
            prepareTexts(ref ret, address.getText(), ix+indent);
            prepareLines(ref ret, address.getLine(), ix+indent);
            prepareCities(ref ret, address.getCity(), ix+indent);
            prepareDiscricts(ref ret, address.getDistrict(), ix+indent);
            prepareStates(ref ret, address.getState(), ix+indent);
            preparePostalCode(ref ret, address.getPostalCode(), ix+indent);
            prepareCountries(ref ret, address.getCountry(), ix+indent);
            preparePeroids(ref ret, address.getPeriod(), ix+indent);
            al(ref ret, "</address>", ix);
        }

        public static void processPhoto(ref string ret, Attachment photo, int ix)
        {
            //TODO
        }

        public static void processLink(ref string ret, Link link, int ix)
        {
            //TODO
        }

        public static void processManagingOrganization(ref string ret, Organization organization, int ix)
        {
            //TODO
        }

        public static void processActive(ref string ret, BoolArray actives, int ix)
        {
            if (actives.Size() > 0)
            {
                al(ref ret, String.Format("<active value=\"{0}\"/>", actives[0]), ix);
            }
        }

        public static void processPatient(ref string ret, Patient Patient, int ix)
        {
            al(ref ret, "<Patient>", ix);
            for (int i = 0; i < Patient.getIdentifier().Size(); i++)
            {
                processIdentifier(ref ret, Patient.getIdentifier()[i], ix+indent);
            }
            for (int i = 0; i < Patient.getName().Size(); i++)
            {
                processHumanName(ref ret, Patient.getName()[i], ix + indent);
            }
            for (int i = 0; i < Patient.getTelecom().Size(); i++)
            {
                processTelecom(ref ret, Patient.getTelecom()[i], ix + indent);
            }
            processGender(ref ret, Patient.getGender(), ix + indent);
            processBirthDate(ref ret, Patient.getBirthData(), ix + indent);
            for (int i = 0; i < Patient.getAddress().Size(); i++)
            {
                processAdress(ref ret, Patient.getAddress()[i], ix + indent);
            }
            for (int i = 0; i < Patient.getManagingOrganization().Size(); i++)
            {
                processManagingOrganization(ref ret, Patient.getManagingOrganization()[i], ix + indent);
            }
            processActive(ref ret, Patient.getActive(), ix + indent);
            for (int i = 0; i < Patient.getPhoto().Size(); i++)
            {
                processPhoto(ref ret, Patient.getPhoto()[i], ix + indent);
            }
            for (int i = 0; i < Patient.getLink().Size(); i++)
            {
                processLink(ref ret, Patient.getLink()[i], ix + indent);
            }
            processCareProviders(ref ret, Patient.getCareProvider(), ix + indent);
            al(ref ret, "</Patient>", ix);
        }

        public static void processPatientsPart(HL7Cb cb, ref string ret, Patients patients, string tmpFile, bool first, bool last)
        {
            if(first)
              al(ref ret, "<Patients xmlns=\"http://hl7.org/fhir\">", 0);
            for (int i = 0; i < patients.Size(); i++)
            {
                if (i % 10 == 0)
                    fflush(ref ret, tmpFile);
                cb.onHL7Progress(100 * i / patients.Size());
                processPatient(ref ret, patients[i], indent);
            }
            if(last)
              al(ref ret, "</Patients>", 0);
            fflush(ref ret, tmpFile);
            cb.onHL7Done(tmpFile);
        }
        public static void processPatients(HL7Cb cb, ref string ret, Patients patients, string tmpFile)
        {
            processPatientsPart(cb, ref ret, patients, tmpFile, true, true);
        }

        public static Patient getEmptyPatient()
        {
            Patient Patient = new Patient(new Identifiers(), new HumanNames(), new ContactPoints(),
                new StringArray(), new StringArray(), new Addresses(), new Attachments(), new CareProviders(), new Organizations(),
                new BoolArray(), new Links());
            return Patient;
        }

        public static StringArray box(string str)
        {
            StringArray ret = new StringArray();
            ret.Insert(str);
            return ret;
        }

        public static Identifier createIndentifier(string system, string value)
        {
            return new Identifier(new StringArray(), new CodeableConcepts(), box(system), box(value), new Periods(), new StringArray());
        }
        public static void addIdentifier(ref Patient Patient, string system, string value)
        {
            Identifiers identifiers = Patient.getIdentifier();
            Identifier indentifier = createIndentifier(system, value);
            Patient.getIdentifier().Insert(indentifier);
        }

        public static HumanNames createHumanNames(string firstName, string family, string secondName = "")
        {
            Patient patient = getEmptyPatient();
            setFirstName(ref patient, firstName);
            if(secondName != "")
                setSecondName(ref patient, secondName);
            setFamily(ref patient, family);
            return patient.getName();
        }

        public static void setFirstName(ref Patient Patient, string firstName)
        {
            HumanNames names = Patient.getName();
            if(names.Size()==0)
            {
                HumanName name = new HumanName(new StringArray(), new StringArray(), new StringArray(), new StringArray(), new StringArray(), new StringArray(), new Periods());
                name.getGiven().Insert(firstName);
                Patient.getName().Insert(name);
            } else
            {
                HumanName name = Patient.getName()[0];
                if(name.getGiven().Size() == 0 )
                {
                    name.getGiven().Insert(firstName);
                } else
                {
                    name.getGiven().Insert(name.getGiven()[0]);
                    name.getGiven()[0] = firstName;
                }
                Patient.getName()[0] = name;
            }
        }

        public static void setSecondName(ref Patient Patient, string secondName)
        {
            HumanNames names = Patient.getName();
            if (names.Size() == 0)
            {
                HumanName name = new HumanName(new StringArray(), new StringArray(), new StringArray(), new StringArray(), new StringArray(), new StringArray(), new Periods());
                name.getGiven().Insert(secondName);
                Patient.getName().Insert(name);
            }
            else
            {
                HumanName name = Patient.getName()[0];
                if (name.getGiven().Size() <= 1)
                {
                    name.getGiven().Insert(secondName);
                }
                else
                {
                    name.getGiven()[1] = secondName;
                }
                Patient.getName()[0] = name;
            }
        }

        public static void setFamily(ref Patient Patient, string family)
        {
            HumanNames names = Patient.getName();
            if (names.Size() == 0)
            {
                HumanName name = new HumanName(new StringArray(), new StringArray(), new StringArray(), new StringArray(), new StringArray(), new StringArray(), new Periods());
                name.getFamily().Insert(family);
                Patient.getName().Insert(name);
            }
            else
            {
                HumanName name = Patient.getName()[0];
                if (name.getFamily().Size() == 0)
                {
                    name.getFamily().Insert(family);
                }
                else
                {
                    name.getFamily()[0] = family;
                }
                Patient.getName()[0] = name;
            }
        }

        public static void setGender(ref Patient Patient, string gender)
        {
            StringArray genders = Patient.getGender();
            if (genders.Size() == 0)
            {
                Patient.getGender().Insert(gender);
            }
            else
            {
                Patient.getGender()[0] = gender;
            }
        }

        public static void addStreetPart(ref Patient Patient, string part)
        {
            Address address;
            if (Patient.getAddress().Size() == 0)
            {
                address = new Address(new StringArray(), new StringArray(), box(part), new StringArray(), new StringArray(),
                    new StringArray(), new StringArray(), new StringArray(), new Periods());
                Patient.getAddress().Insert(address);
            } else
            {
                address = Patient.getAddress()[0];
                address.getLine().Insert(part);
                Patient.getAddress()[0] = address;
            }
            
        }

        public static void setPostalCode(ref Patient Patient, string postalCode)
        {
            Address address;
            if (Patient.getAddress().Size() == 0)
            {
                address = new Address(new StringArray(), new StringArray(), new StringArray(), new StringArray(), new StringArray(),
                    new StringArray(), box(postalCode), new StringArray(), new Periods());
                Patient.getAddress().Insert(address);
            }
            else
            {
                address = Patient.getAddress()[0];
                if(address.getPostalCode().Size() == 0)
                    address.getPostalCode().Insert(postalCode);
                else
                    address.getPostalCode()[0]=postalCode;
                Patient.getAddress()[0] = address;
            }

        }

        public static void setCity(ref Patient Patient, string city)
        {
            Address address;
            if (Patient.getAddress().Size() == 0)
            {
                address = new Address(new StringArray(), new StringArray(), new StringArray(), box(city), new StringArray(),
                    new StringArray(), new StringArray(), new StringArray(), new Periods());
                Patient.getAddress().Insert(address);
            }
            else
            {
                address = Patient.getAddress()[0];
                if (address.getCity().Size() == 0)
                    address.getCity().Insert(city);
                else
                    address.getCity()[0] = city;
                Patient.getAddress()[0] = address;
            }

        }

        public static void setCountry(ref Patient Patient, string country)
        {
            Address address;
            if (Patient.getAddress().Size() == 0)
            {
                address = new Address(new StringArray(), new StringArray(), new StringArray(), new StringArray(), new StringArray(),
                    new StringArray(), new StringArray(), box(country), new Periods());
                Patient.getAddress().Insert(address);
            }
            else
            {
                address = Patient.getAddress()[0];
                if (address.getCountry().Size() == 0)
                    address.getCountry().Insert(country);
                else
                    address.getCountry()[0] = country;
                Patient.getAddress()[0] = address;
            }

        }

        public static void setVatCode(ref Patient Patient, string vatCode)
        {
            string vatCodeKeyWord = "VatCode";
            Identifiers identifiers = Patient.getIdentifier();
            for(int i=0;i<identifiers.Size();i++)
            {
                if(identifiers[i].getSystem().Size()>0 && identifiers[i].getSystem()[0] == vatCodeKeyWord)
                {
                    if (identifiers[i].getValue().Size() == 0)
                      identifiers[i].getValue().Insert(vatCode);
                    else
                      identifiers[i].getValue()[0] = vatCode;
                    return;
                }
            }
            Identifier vatCodeObj = new Identifier(new StringArray(), new CodeableConcepts(), box(vatCodeKeyWord), box(vatCode), new Periods(), new StringArray());
            Patient.getIdentifier().Insert(vatCodeObj);
        }

        public static void setPatientalCode(ref Patient Patient, string PatientalCode)
        {
            string PatientalCodeKeyWord = "PatientalCode";
            Identifiers identifiers = Patient.getIdentifier();
            for (int i = 0; i < identifiers.Size(); i++)
            {
                if (identifiers[i].getSystem().Size() > 0 && identifiers[i].getSystem()[0] == PatientalCodeKeyWord)
                {
                    if (identifiers[i].getValue().Size() == 0)
                        identifiers[i].getValue().Insert(PatientalCode);
                    else
                        identifiers[i].getValue()[0] = PatientalCode;
                    return;
                }
            }
            Identifier vatCodeObj = new Identifier(new StringArray(), new CodeableConcepts(), box(PatientalCodeKeyWord), box(PatientalCode), new Periods(), new StringArray());
            Patient.getIdentifier().Insert(vatCodeObj);
        }

        public static void addPractitioner(ref Patient patient, string name, string family, string pesel, string phone, string role, string city, string postalCode, string addressPart1, string addressPart2="", string addressPart3="")
        {
            CareProviders providers = patient.getCareProvider();
            Patient tmppatient = getEmptyPatient();
            setPostalCode(ref tmppatient, postalCode);
            setCity(ref tmppatient, city);
            addStreetPart(ref tmppatient, addressPart1);
            addStreetPart(ref tmppatient, addressPart2);
            addStreetPart(ref tmppatient, addressPart3);
            Practitioner practitioner = new Practitioner(new Identifiers(), createHumanNames(name, family, ""), new ContactPoints(),
                new StringArray(), new StringArray(), tmppatient.getAddress(), new PractitionerRoles());
            practitioner.getIdentifier().Insert(createIndentifier("PatientalCode", pesel));
            practitioner.getTelecom().Insert(createContactPoint("phone", phone));
            StringArray arr1 = new StringArray();
            arr1.Insert(role);
            practitioner.getPractitionerRole().Insert(new PractitionerRole(new CodeableConcept(arr1)));
            
            providers.Insert(CareProvider.createPractitioner(practitioner));
        }

        public static void setBirthDate(ref Patient Patient, string birthDate)
        {
            if (Patient.getBirthData().Size() == 0)
            {
                Patient.getBirthData().Insert(birthDate);
            }
            else
            {
                Patient.getBirthData()[0] = birthDate;
            }
        }

        public static ContactPoint createContactPoint(string system, string value)
        {
            return new ContactPoint(box(system), box(value), new StringArray(), new Periods());
        }
        public static void addContact(ref Patient Patient, string system, string value)
        {
            Patient.getTelecom().Insert(createContactPoint(system, value));
        }

    }
}
/*




*/
