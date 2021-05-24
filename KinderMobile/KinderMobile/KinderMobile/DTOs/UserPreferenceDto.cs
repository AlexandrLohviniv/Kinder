using System;
using System.Collections.Generic;
using System.Text;

namespace KinderMobile.DTOs
{
    public class UserPreferenceDto
    {
        public Rate SmokeRate { get; set; }
        public Rate BabyRate { get; set; }
        public int HeightRate { get; set; }
        public Rate PetsRate { get; set; }
        public Rate RelationshipRate { get; set; }
        public Sexuality Sex { get; set; }
        public Rate DrinkingRate { get; set; }
    }
}
