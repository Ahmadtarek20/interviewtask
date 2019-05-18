﻿using System;

namespace ITI.Enterprise.InterviewTask.DomainClasses
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Alternative { get; set; }

        public Image()
        {
            Id = Guid.NewGuid();       
        }
    }
}
