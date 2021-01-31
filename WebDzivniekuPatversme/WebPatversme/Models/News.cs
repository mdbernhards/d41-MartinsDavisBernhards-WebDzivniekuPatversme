﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WebPatversme.Models
{
    public class News
    {
        [Key]
        public string NewsID { set; get; }

        public string Text { set; get; }

        public DateTime DateCreated { set; get; }

        public string ImagePath { set; get; }
    }
}