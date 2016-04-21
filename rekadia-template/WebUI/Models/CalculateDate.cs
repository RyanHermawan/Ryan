using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class CalculateDate
    {
        private int[] monthDay = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        private DateTime fromDate;
        private DateTime toDate;
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }

        public CalculateDate() { }

        public CalculateDate (DateTime d1, DateTime d2)
        {
            if (d1 > d2)
            {
                this.fromDate = d2;
                this.toDate = d1;
            }
            else
            {
                this.fromDate = d1;
                this.toDate = d2;
            } 
            ///////////////////
            //DAY CALCULATION//
            ///////////////////
            
            //If ‘this.fromDate.Day’ is greater than ‘this.toDate.Day’, 
            //then we store the value in ‘increment’ which will be added to ‘this.toDate.Day’. 
            //To get the proper number ‘int[] monthDay’ will help us.
            var increment = 0;
            if (this.fromDate.Day > this.toDate.Day)
            {
                increment = this.monthDay[this.fromDate.Month - 1];
            }
            
            if (increment == -1)
            {
                if (DateTime.IsLeapYear(this.fromDate.Year))
                {
                    increment = 29;
                }
                else
                {
                    increment = 28;
                }
            }

            if (increment != 0)
            {
                day = (this.toDate.Day + increment) - this.fromDate.Day;
                increment = 1;
            }
            else
            {
                day = this.toDate.Day - this.fromDate.Day;
            }

            if ( d2 > d1 )
            {
                day *= -1;
            }

            /////////////////////
            //MONTH CALCULATION//
            /////////////////////

            //Month calculation is very simple and almost like Day calculation. 
            //Here if ‘this.toDate.Month’ is smaller than the result of ( ‘this.fromDate.Month’ + ‘increment’), 
            //then just add ‘12’ to ‘this.toDate.Month’ and add ‘1’ to ‘this.fromDate.Year’ (which is stored in ‘increment’); 
            //Otherwise, it is just simple subtraction.
            if ((this.fromDate.Month + increment) > this.toDate.Month)
            {
                this.month = (this.toDate.Month + 12) - (this.fromDate.Month + increment);
                increment = 1;
            }
            else
            {
                this.month = (this.toDate.Month) - (this.fromDate.Month + increment);
                increment = 0;
            }

            if (d2 > d1)
            {
                month *= -1;
            }

            ////////////////////
            //YEAR CALCULATION//
            ////////////////////

            //This is just simple arithmetic operation. Nothing to say.
            this.year = this.toDate.Year - (this.fromDate.Year + increment);

            if (d2 > d1)
            {
                year *= -1;
            }

            ///////////////////////////////
            //HOUR AND MINUTE CALCULATION//
            ///////////////////////////////

            //This is just simple arithmetic operation. Nothing to say.
            TimeSpan tim = d1 - d2;
            this.hour = tim.Hours;
            this.minute = tim.Minutes;
        }

        public override string ToString()
        {    
            return this.year + " Year(s), " + this.month + " month(s), " + this.day + " day(s), "
                + this.hour + " Hour(s), " + this.minute + " minute(s)";
        } 

    }
}