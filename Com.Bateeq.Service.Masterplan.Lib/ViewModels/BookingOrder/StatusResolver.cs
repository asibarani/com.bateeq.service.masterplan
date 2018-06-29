﻿using AutoMapper;
using System;

namespace Com.Bateeq.Service.Masterplan.Lib.ViewModels.BookingOrder
{
    public class StatusResolver : IValueResolver<Models.BookingOrder, BookingOrderViewModel, string>
    {
        public string Resolve(Models.BookingOrder source, BookingOrderViewModel destination, string destMember, ResolutionContext context)
        {
            try
            {
                if (source.DetailConfirms.Count <= 0)
                    return StatusConst.BOOKING;
                else
                    return StatusConst.CONFIRMED;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}