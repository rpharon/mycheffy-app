using DynamicData.Binding;
using mycheffy.Models.Promo;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace mycheffy.ViewModels.Offers
{
    [DataContract]
    public class OffersPromoTabViewModel : NavigationViewModelBase
    {
        [DataMember]
        [Reactive]
        public IEnumerable<PromoModel> Promos { get; set; }

        OffersPromoTabViewModel()
        {
            List<PromoModel> listPromo = new List<PromoModel>();

            listPromo.Add(new PromoModel("id-1", "GCash Promo", "FREEDGCASH", "globe",
                "Get Unlimited free delivery using GCASH",
                "Use code FREEDGCASH & get free delivery on all orders above ₱500",
                "From now until December 31, 2020, enjoy free delivery on any store on weekdays from 9 a.m. to 8 p.m. when you choose Gcash as your payment method."));

            listPromo.Add(new PromoModel("id-2", "Paymaya Promo", "FIRSTUSER", "smart",
                "Get 15 % off for first time users",
                "Use code FIRSTUSER & get free delivery on all orders above ₱500",
                "From now until December 31, 2020, enjoy free delivery on any store on weekdays from 9 a.m. to 8 p.m. when you choose Paymaya as your payment method."));

            listPromo.Add(new PromoModel("id-3", "GPay Promo", "BUY1GET1", "google_pay",
                "Get one meal free when you order more than ₱1000",
                "Use code BUY1GET1 & get free delivery on all orders above ₱500",
                "From now until December 31, 2020, enjoy free delivery on any store on weekdays from 9 a.m. to 8 p.m. when you choose Google as your payment method."));

            Promos = listPromo;
        }
    }
}
