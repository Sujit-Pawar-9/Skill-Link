namespace BLL;
using BOL;
using DAL;
using Org.BouncyCastle.Ocsp;

public static class BookingListManager{
    public static bool AddIntoBookingList(int RequirementID,int UserID, BOL.ServiceProvider serviceProvider){
        return DBManager.AddBookingEntry(RequirementID,UserID,serviceProvider);
    }
    public static List<BookingList> GetBookingListByUserId(int UserID){
        return DBManager.GetBookingListByUserId(UserID);
    }
    
    public static bool SelectServiceProvider(int UserID,string ServiceProviderUsername,bool IsSelected){
        return DBManager.SelectServiceProvider(UserID,ServiceProviderUsername,IsSelected);
    }
   
}
