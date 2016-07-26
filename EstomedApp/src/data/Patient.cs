using System;
using System.Collections.Generic;
using System.Text;
public class StringArray
{
    private List<String> _table = new List<String>();
    public StringArray() { }
    public int Size()
    {
        return _table.Count;
    }
    public void Insert(String _v)
    {
        _table.Add(_v);
    }
    public String this[int _ix]
    {
        get
        {
            return _table[_ix];
        }
        set
        {
            _table[_ix] = value;
        }
    }
    public String Get(int _ix)
    {
        return _table[_ix];
    }
    public void Delete(int _ix)
    {
        _table.RemoveAt(_ix);
    }
}
public class BoolArray
{
    private List<bool> _table = new List<bool>();
    public BoolArray() { }
    public int Size()
    {
        return _table.Count;
    }
    public void Insert(bool _v)
    {
        _table.Add(_v);
    }
    public bool this[int _ix]
    {
        get
        {
            return _table[_ix];
        }
        set
        {
            _table[_ix] = value;
        }
    }
    public bool Get(int _ix)
    {
        return _table[_ix];
    }
    public void Delete(int _ix)
    {
        _table.RemoveAt(_ix);
    }
}
public class CodeableConcept
{
    private StringArray text;
    public CodeableConcept(StringArray _text)
    {
        this.text = _text;
    }
    public StringArray getText()
    {
        return text;
    }
    public void setText(StringArray _v)
    {
        text = _v;
    }
}
public class CodeableConcepts
{
    private List<CodeableConcept> _table = new List<CodeableConcept>();
    public CodeableConcepts() { }
    public int Size()
    {
        return _table.Count;
    }
    public void Insert(CodeableConcept _v)
    {
        _table.Add(_v);
    }
    public CodeableConcept this[int _ix]
    {
        get
        {
            return _table[_ix];
        }
        set
        {
            _table[_ix] = value;
        }
    }
    public CodeableConcept Get(int _ix)
    {
        return _table[_ix];
    }
    public void Delete(int _ix)
    {
        _table.RemoveAt(_ix);
    }
}
public class Period
{
    private StringArray start;
    private StringArray end;
    public Period(StringArray _start, StringArray _end)
    {
        this.start = _start;
        this.end = _end;
    }
    public StringArray getStart()
    {
        return start;
    }
    public void setStart(StringArray _v)
    {
        start = _v;
    }
    public StringArray getEnd()
    {
        return end;
    }
    public void setEnd(StringArray _v)
    {
        end = _v;
    }
}
public class Periods
{
    private List<Period> _table = new List<Period>();
    public Periods() { }
    public int Size()
    {
        return _table.Count;
    }
    public void Insert(Period _v)
    {
        _table.Add(_v);
    }
    public Period this[int _ix]
    {
        get
        {
            return _table[_ix];
        }
        set
        {
            _table[_ix] = value;
        }
    }
    public Period Get(int _ix)
    {
        return _table[_ix];
    }
    public void Delete(int _ix)
    {
        _table.RemoveAt(_ix);
    }
}
public class Identifier
{
    private StringArray use;
    private CodeableConcepts type;
    private StringArray system;
    private StringArray value;
    private Periods period;
    private StringArray assigner;
    public Identifier(StringArray _use, CodeableConcepts _type, StringArray _system, StringArray _value, Periods _period, StringArray _assigner)
    {
        this.use = _use;
        this.type = _type;
        this.system = _system;
        this.value = _value;
        this.period = _period;
        this.assigner = _assigner;
    }
    public StringArray getUse()
    {
        return use;
    }
    public void setUse(StringArray _v)
    {
        use = _v;
    }
    public CodeableConcepts getType()
    {
        return type;
    }
    public void setType(CodeableConcepts _v)
    {
        type = _v;
    }
    public StringArray getSystem()
    {
        return system;
    }
    public void setSystem(StringArray _v)
    {
        system = _v;
    }
    public StringArray getValue()
    {
        return value;
    }
    public void setValue(StringArray _v)
    {
        value = _v;
    }
    public Periods getPeriod()
    {
        return period;
    }
    public void setPeriod(Periods _v)
    {
        period = _v;
    }
    public StringArray getAssigner()
    {
        return assigner;
    }
    public void setAssigner(StringArray _v)
    {
        assigner = _v;
    }
}
public class Identifiers
{
    private List<Identifier> _table = new List<Identifier>();
    public Identifiers() { }
    public int Size()
    {
        return _table.Count;
    }
    public void Insert(Identifier _v)
    {
        _table.Add(_v);
    }
    public Identifier this[int _ix]
    {
        get
        {
            return _table[_ix];
        }
        set
        {
            _table[_ix] = value;
        }
    }
    public Identifier Get(int _ix)
    {
        return _table[_ix];
    }
    public void Delete(int _ix)
    {
        _table.RemoveAt(_ix);
    }
}
public class HumanName
{
    private StringArray use;
    private StringArray text;
    private StringArray family;
    private StringArray given;
    private StringArray prefix;
    private StringArray suffix;
    private Periods period;
    public HumanName(StringArray _use, StringArray _text, StringArray _family, StringArray _given, StringArray _prefix, StringArray _suffix, Periods _period)
    {
        this.use = _use;
        this.text = _text;
        this.family = _family;
        this.given = _given;
        this.prefix = _prefix;
        this.suffix = _suffix;
        this.period = _period;
    }
    public StringArray getUse()
    {
        return use;
    }
    public void setUse(StringArray _v)
    {
        use = _v;
    }
    public StringArray getText()
    {
        return text;
    }
    public void setText(StringArray _v)
    {
        text = _v;
    }
    public StringArray getFamily()
    {
        return family;
    }
    public void setFamily(StringArray _v)
    {
        family = _v;
    }
    public StringArray getGiven()
    {
        return given;
    }
    public void setGiven(StringArray _v)
    {
        given = _v;
    }
    public StringArray getPrefix()
    {
        return prefix;
    }
    public void setPrefix(StringArray _v)
    {
        prefix = _v;
    }
    public StringArray getSuffix()
    {
        return suffix;
    }
    public void setSuffix(StringArray _v)
    {
        suffix = _v;
    }
    public Periods getPeriod()
    {
        return period;
    }
    public void setPeriod(Periods _v)
    {
        period = _v;
    }
}
public class HumanNames
{
    private List<HumanName> _table = new List<HumanName>();
    public HumanNames() { }
    public int Size()
    {
        return _table.Count;
    }
    public void Insert(HumanName _v)
    {
        _table.Add(_v);
    }
    public HumanName this[int _ix]
    {
        get
        {
            return _table[_ix];
        }
        set
        {
            _table[_ix] = value;
        }
    }
    public HumanName Get(int _ix)
    {
        return _table[_ix];
    }
    public void Delete(int _ix)
    {
        _table.RemoveAt(_ix);
    }
}
public class ContactPoint
{
    private StringArray system;
    private StringArray value;
    private StringArray use;
    private Periods period;
    public ContactPoint(StringArray _system, StringArray _value, StringArray _use, Periods _period)
    {
        this.system = _system;
        this.value = _value;
        this.use = _use;
        this.period = _period;
    }
    public StringArray getSystem()
    {
        return system;
    }
    public void setSystem(StringArray _v)
    {
        system = _v;
    }
    public StringArray getValue()
    {
        return value;
    }
    public void setValue(StringArray _v)
    {
        value = _v;
    }
    public StringArray getUse()
    {
        return use;
    }
    public void setUse(StringArray _v)
    {
        use = _v;
    }
    public Periods getPeriod()
    {
        return period;
    }
    public void setPeriod(Periods _v)
    {
        period = _v;
    }
}
public class ContactPoints
{
    private List<ContactPoint> _table = new List<ContactPoint>();
    public ContactPoints() { }
    public int Size()
    {
        return _table.Count;
    }
    public void Insert(ContactPoint _v)
    {
        _table.Add(_v);
    }
    public ContactPoint this[int _ix]
    {
        get
        {
            return _table[_ix];
        }
        set
        {
            _table[_ix] = value;
        }
    }
    public ContactPoint Get(int _ix)
    {
        return _table[_ix];
    }
    public void Delete(int _ix)
    {
        _table.RemoveAt(_ix);
    }
}
public class Address
{
    private StringArray use;
    private StringArray text;
    private StringArray line;
    private StringArray city;
    private StringArray district;
    private StringArray state;
    private StringArray postalCode;
    private StringArray country;
    private Periods period;
    public Address(StringArray _use, StringArray _text, StringArray _line, StringArray _city, StringArray _district, StringArray _state, StringArray _postalCode, StringArray _country, Periods _period)
    {
        this.use = _use;
        this.text = _text;
        this.line = _line;
        this.city = _city;
        this.district = _district;
        this.state = _state;
        this.postalCode = _postalCode;
        this.country = _country;
        this.period = _period;
    }
    public StringArray getUse()
    {
        return use;
    }
    public void setUse(StringArray _v)
    {
        use = _v;
    }
    public StringArray getText()
    {
        return text;
    }
    public void setText(StringArray _v)
    {
        text = _v;
    }
    public StringArray getLine()
    {
        return line;
    }
    public void setLine(StringArray _v)
    {
        line = _v;
    }
    public StringArray getCity()
    {
        return city;
    }
    public void setCity(StringArray _v)
    {
        city = _v;
    }
    public StringArray getDistrict()
    {
        return district;
    }
    public void setDistrict(StringArray _v)
    {
        district = _v;
    }
    public StringArray getState()
    {
        return state;
    }
    public void setState(StringArray _v)
    {
        state = _v;
    }
    public StringArray getPostalCode()
    {
        return postalCode;
    }
    public void setPostalCode(StringArray _v)
    {
        postalCode = _v;
    }
    public StringArray getCountry()
    {
        return country;
    }
    public void setCountry(StringArray _v)
    {
        country = _v;
    }
    public Periods getPeriod()
    {
        return period;
    }
    public void setPeriod(Periods _v)
    {
        period = _v;
    }
}
public class Addresses
{
    private List<Address> _table = new List<Address>();
    public Addresses() { }
    public int Size()
    {
        return _table.Count;
    }
    public void Insert(Address _v)
    {
        _table.Add(_v);
    }
    public Address this[int _ix]
    {
        get
        {
            return _table[_ix];
        }
        set
        {
            _table[_ix] = value;
        }
    }
    public Address Get(int _ix)
    {
        return _table[_ix];
    }
    public void Delete(int _ix)
    {
        _table.RemoveAt(_ix);
    }
}
public class Organization
{
    private String nothing;
    public Organization(String _nothing)
    {
        this.nothing = _nothing;
    }
    public String getNothing()
    {
        return nothing;
    }
    public void setNothing(String _v)
    {
        nothing = _v;
    }
}
public class Organizations
{
    private List<Organization> _table = new List<Organization>();
    public Organizations() { }
    public int Size()
    {
        return _table.Count;
    }
    public void Insert(Organization _v)
    {
        _table.Add(_v);
    }
    public Organization this[int _ix]
    {
        get
        {
            return _table[_ix];
        }
        set
        {
            _table[_ix] = value;
        }
    }
    public Organization Get(int _ix)
    {
        return _table[_ix];
    }
    public void Delete(int _ix)
    {
        _table.RemoveAt(_ix);
    }
}
public class PractitionerRole
{
    private CodeableConcept role;
    public PractitionerRole(CodeableConcept _role)
    {
        this.role = _role;
    }
    public CodeableConcept getRole()
    {
        return role;
    }
    public void setRole(CodeableConcept _v)
    {
        role = _v;
    }
}
public class PractitionerRoles
{
    private List<PractitionerRole> _table = new List<PractitionerRole>();
    public PractitionerRoles() { }
    public int Size()
    {
        return _table.Count;
    }
    public void Insert(PractitionerRole _v)
    {
        _table.Add(_v);
    }
    public PractitionerRole this[int _ix]
    {
        get
        {
            return _table[_ix];
        }
        set
        {
            _table[_ix] = value;
        }
    }
    public PractitionerRole Get(int _ix)
    {
        return _table[_ix];
    }
    public void Delete(int _ix)
    {
        _table.RemoveAt(_ix);
    }
}
public class Practitioner
{
    private Identifiers identifier;
    private HumanNames name;
    private ContactPoints telecom;
    private StringArray gender;
    private StringArray birthData;
    private Addresses address;
    private PractitionerRoles practitionerRole;
    public Practitioner(Identifiers _identifier, HumanNames _name, ContactPoints _telecom, StringArray _gender, StringArray _birthData, Addresses _address, PractitionerRoles _practitionerRole)
    {
        this.identifier = _identifier;
        this.name = _name;
        this.telecom = _telecom;
        this.gender = _gender;
        this.birthData = _birthData;
        this.address = _address;
        this.practitionerRole = _practitionerRole;
    }
    public Identifiers getIdentifier()
    {
        return identifier;
    }
    public void setIdentifier(Identifiers _v)
    {
        identifier = _v;
    }
    public HumanNames getName()
    {
        return name;
    }
    public void setName(HumanNames _v)
    {
        name = _v;
    }
    public ContactPoints getTelecom()
    {
        return telecom;
    }
    public void setTelecom(ContactPoints _v)
    {
        telecom = _v;
    }
    public StringArray getGender()
    {
        return gender;
    }
    public void setGender(StringArray _v)
    {
        gender = _v;
    }
    public StringArray getBirthData()
    {
        return birthData;
    }
    public void setBirthData(StringArray _v)
    {
        birthData = _v;
    }
    public Addresses getAddress()
    {
        return address;
    }
    public void setAddress(Addresses _v)
    {
        address = _v;
    }
    public PractitionerRoles getPractitionerRole()
    {
        return practitionerRole;
    }
    public void setPractitionerRole(PractitionerRoles _v)
    {
        practitionerRole = _v;
    }
}
public class CareProvider
{
    private int _type;
    private Object _ptrObj;

    private const int _TypeOrganization = 0;
    private const int _TypePractitioner = 1;

    private CareProvider() { }
    public bool isOrganization()
    {
        return _type == _TypeOrganization;
    }
    public bool isPractitioner()
    {
        return _type == _TypePractitioner;
    }

    public Organization asOrganization()
    {
        if (_type != _TypeOrganization)
            throw new Exception("CareProvider::asOrganization");
        return (Organization)_ptrObj;
    }
    public Practitioner asPractitioner()
    {
        if (_type != _TypePractitioner)
            throw new Exception("CareProvider::asPractitioner");
        return (Practitioner)_ptrObj;
    }

    static public CareProvider createOrganization(Organization _v)
    {
        CareProvider _value = new CareProvider();
        _value._type = _TypeOrganization;
        _value._ptrObj = _v;
        return _value;
    }
    static public CareProvider createPractitioner(Practitioner _v)
    {
        CareProvider _value = new CareProvider();
        _value._type = _TypePractitioner;
        _value._ptrObj = _v;
        return _value;
    }
}
public class CareProviders
{
    private List<CareProvider> _table = new List<CareProvider>();
    public CareProviders() { }
    public int Size()
    {
        return _table.Count;
    }
    public void Insert(CareProvider _v)
    {
        _table.Add(_v);
    }
    public CareProvider this[int _ix]
    {
        get
        {
            return _table[_ix];
        }
        set
        {
            _table[_ix] = value;
        }
    }
    public CareProvider Get(int _ix)
    {
        return _table[_ix];
    }
    public void Delete(int _ix)
    {
        _table.RemoveAt(_ix);
    }
}
public class Attachment
{
    private String nothing;
    public Attachment(String _nothing)
    {
        this.nothing = _nothing;
    }
    public String getNothing()
    {
        return nothing;
    }
    public void setNothing(String _v)
    {
        nothing = _v;
    }
}
public class Attachments
{
    private List<Attachment> _table = new List<Attachment>();
    public Attachments() { }
    public int Size()
    {
        return _table.Count;
    }
    public void Insert(Attachment _v)
    {
        _table.Add(_v);
    }
    public Attachment this[int _ix]
    {
        get
        {
            return _table[_ix];
        }
        set
        {
            _table[_ix] = value;
        }
    }
    public Attachment Get(int _ix)
    {
        return _table[_ix];
    }
    public void Delete(int _ix)
    {
        _table.RemoveAt(_ix);
    }
}
public class Link
{
    private String nothing;
    public Link(String _nothing)
    {
        this.nothing = _nothing;
    }
    public String getNothing()
    {
        return nothing;
    }
    public void setNothing(String _v)
    {
        nothing = _v;
    }
}
public class Links
{
    private List<Link> _table = new List<Link>();
    public Links() { }
    public int Size()
    {
        return _table.Count;
    }
    public void Insert(Link _v)
    {
        _table.Add(_v);
    }
    public Link this[int _ix]
    {
        get
        {
            return _table[_ix];
        }
        set
        {
            _table[_ix] = value;
        }
    }
    public Link Get(int _ix)
    {
        return _table[_ix];
    }
    public void Delete(int _ix)
    {
        _table.RemoveAt(_ix);
    }
}
public class Patient
{
    private Identifiers identifier;
    private HumanNames name;
    private ContactPoints telecom;
    private StringArray gender;
    private StringArray birthData;
    private Addresses address;
    private Attachments photo;
    private CareProviders careProvider;
    private Organizations managingOrganization;
    private BoolArray active;
    private Links link;
    public Patient(Identifiers _identifier, HumanNames _name, ContactPoints _telecom, StringArray _gender, StringArray _birthData, Addresses _address, Attachments _photo, CareProviders _careProvider, Organizations _managingOrganization, BoolArray _active, Links _link)
    {
        this.identifier = _identifier;
        this.name = _name;
        this.telecom = _telecom;
        this.gender = _gender;
        this.birthData = _birthData;
        this.address = _address;
        this.photo = _photo;
        this.careProvider = _careProvider;
        this.managingOrganization = _managingOrganization;
        this.active = _active;
        this.link = _link;
    }
    public Identifiers getIdentifier()
    {
        return identifier;
    }
    public void setIdentifier(Identifiers _v)
    {
        identifier = _v;
    }
    public HumanNames getName()
    {
        return name;
    }
    public void setName(HumanNames _v)
    {
        name = _v;
    }
    public ContactPoints getTelecom()
    {
        return telecom;
    }
    public void setTelecom(ContactPoints _v)
    {
        telecom = _v;
    }
    public StringArray getGender()
    {
        return gender;
    }
    public void setGender(StringArray _v)
    {
        gender = _v;
    }
    public StringArray getBirthData()
    {
        return birthData;
    }
    public void setBirthData(StringArray _v)
    {
        birthData = _v;
    }
    public Addresses getAddress()
    {
        return address;
    }
    public void setAddress(Addresses _v)
    {
        address = _v;
    }
    public Attachments getPhoto()
    {
        return photo;
    }
    public void setPhoto(Attachments _v)
    {
        photo = _v;
    }
    public CareProviders getCareProvider()
    {
        return careProvider;
    }
    public void setCareProvider(CareProviders _v)
    {
        careProvider = _v;
    }
    public Organizations getManagingOrganization()
    {
        return managingOrganization;
    }
    public void setManagingOrganization(Organizations _v)
    {
        managingOrganization = _v;
    }
    public BoolArray getActive()
    {
        return active;
    }
    public void setActive(BoolArray _v)
    {
        active = _v;
    }
    public Links getLink()
    {
        return link;
    }
    public void setLink(Links _v)
    {
        link = _v;
    }
}
public class Patients
{
    private List<Patient> _table = new List<Patient>();
    public Patients() { }
    public int Size()
    {
        return _table.Count;
    }
    public void Insert(Patient _v)
    {
        _table.Add(_v);
    }
    public Patient this[int _ix]
    {
        get
        {
            return _table[_ix];
        }
        set
        {
            _table[_ix] = value;
        }
    }
    public Patient Get(int _ix)
    {
        return _table[_ix];
    }
    public void Delete(int _ix)
    {
        _table.RemoveAt(_ix);
    }
}
