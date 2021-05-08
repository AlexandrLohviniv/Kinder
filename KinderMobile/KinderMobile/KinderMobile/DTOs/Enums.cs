using System;
using System.Collections.Generic;
using System.Text;

namespace KinderMobile.DTOs
{
    public enum Role
    {
        SimpleUser,
        Admin
    }
    public enum Sexuality
    {
        Male,
        Female,
        NotDefined
    }
    public enum Rate
    {
        Positive,
        Negative,
        Neutral
    }

    public enum InputType 
    {
        Age,
        Height,
        Date,
        Name,
        Default
    }
}
