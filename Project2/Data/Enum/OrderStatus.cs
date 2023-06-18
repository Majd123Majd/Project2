namespace Project2.Data.Enum
{
    public enum OrderStatus
    {
        Unknown = 0,
        Acceptance,//قبول الطلب
        Unacceptable,//رفض الطلب
        WaitingForAccept,//قيد الانتظار
        DeliveredToDeliver,//تسليم الطلب لشركة التوصيل
        Delivered,//تسليم الطلب ل الزبون
        ReceivedFromMarketer,//استلام الطلب من البائع
        Received //استلام الطلب من شركة التوصيل
    }
}

//WaitingForAccept و عند الزبون  Unknown قبل قبول الطلب : عند البائع 
//Unknown و عند شركة التوصيل  Acceptance  و عند الزبون  Acceptance  بعد قبول الطلب : عند البائع   
//Acceptance عند قبول شركة التوصيل بصير عندها 
//ReceivedFromMarketer و عند شركة التوصيل DeliveredToDeliver اخذ الطلب من البائع : عند البائع 
//Received و عند الزبون Delivered عند التسليم للزبون : عند البائع و شركة التوصيل