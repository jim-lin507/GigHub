﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; set; }

        protected UserNotification()
        {

        }
        public UserNotification(ApplicationUser user, Notification notification)
        {
            if(user == null)
                throw new ArgumentException("user");
            if(notification == null)
                throw new ArgumentException("notification");
            User = user;
            Notification = notification;
        }

        public void Read()
        {
            IsRead = true;
        }
    }
}