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
        Neutral,
        Negative
       
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
