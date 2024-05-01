using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL;
public class UserRequirement{
        [Key]
        public int RequirementID { get; set; }
        public int UserID { get; set; }
        public string Skills { get; set; }
        public string Wages { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    public UserRequirement(){
        
    }
    public UserRequirement(int userid,string skills,string wages,string address,string date){
        UserID=userid;
        Skills=skills;
        Wages=wages;
        Address=address;
        Date=date;
    }
    public UserRequirement(string skills,string wages,string address,string date){
        Skills=skills;
        Wages=wages;
        Address=address;
        Date=date;
    }
}