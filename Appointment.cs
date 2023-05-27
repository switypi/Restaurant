
using System;
/// <summary>
/// This class is actually a stripped-down version of the actual Appointment class, which was generated using the 
/// Linq-to-SQL Designer (essentially a Linq ORM to the Appointments table in the db)
/// </summary>
/// <remarks>Obviously, you should use your own appointment object/classes, and change the code-behind in MonthView.xaml.vb to
/// support a List(Of T) where T is whatever the System.Type is for your appointment class.
/// </remarks>
/// <author>Kirk Davis, February 2009 (in like, 4 hours, and it shows!)</author>
class Appointment
{

    private int _AppointmentID;
    private string _Subject;
    private string _Location;
    private string _Details;
    private System.DateTime? _StartTime;
    private System.DateTime? _EndTime;
    private System.DateTime _reccreatedDate;


    public Appointment() : base()
    {
    }

    public int AppointmentID
    {
        get
        {
            return _AppointmentID;
        }
        set
        {
            if (_AppointmentID == value == false)
            {
                _AppointmentID = value;
            }
        }
    }

    public string Subject
    {
        get
        {
            return _Subject;
        }
        set
        {
            if (string.Equals(_Subject, value) == false)
            {
                _Subject = value;
            }
        }
    }

    public string Location
    {
        get
        {
            return _Location;
        }
        set
        {
            if (string.Equals(_Location, value) == false)
            {
                _Location = value;
            }
        }
    }

    public string Details
    {
        get
        {
            return _Details;
        }
        set
        {
            if (string.Equals(_Details, value) == false)
            {
                _Details = value;
            }
        }
    }

    public System.DateTime? StartTime
    {
        get
        {
            return _StartTime;
        }
        set
        {
            if (_StartTime.Equals(value) == false)
            {
                _StartTime = value;
            }
        }
    }

    public System.DateTime? EndTime
    {
        get
        {
            return _EndTime;
        }
        set
        {
            if (_EndTime.Equals(value) == false)
            {
                _EndTime = value;
            }
        }
    }

    public DateTime reccreatedDate
    {
        get
        {
            return _reccreatedDate;
        }
        set
        {
            if (_reccreatedDate == value == false)
            {
                _reccreatedDate = value;
            }
        }
    }

}