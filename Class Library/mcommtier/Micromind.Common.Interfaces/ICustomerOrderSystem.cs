using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICustomerOrderSystem
	{
		CustomerOrderData GetOrderSummary(CustomerOrderData customerOrderData);

		int AddCustomerOrder(CustomerOrderData customerOrderData, ShipmentAddressData shipmentAddressData);

		int AddCustomerOrder(CustomerOrderData customerOrderData, ShipmentAddressData shipmentAddressData, CustomFieldData customFieldData);

		int AddCustomerOrder(CustomerOrderData customerOrderData);

		int AddCustomerOrder(CustomerOrderData customerOrderData, CustomFieldData customFieldData);

		bool UpdateCustomerOrder(CustomerOrderData customerOrderData, ShipmentAddressData shipmentAddressData);

		bool UpdateCustomerOrder(CustomerOrderData customerOrderData, ShipmentAddressData shipmentAddressData, CustomFieldData customFieldData);

		bool UpdateCustomerOrder(CustomerOrderData customerOrderData);

		bool UpdateCustomerOrder(CustomerOrderData customerOrderData, CustomFieldData customFieldData);

		string GetAutoOrderNumber();

		string GetAutoQuoteNumber();

		string GetAutoDeliveryNoteNumber();

		bool ExistCustomerOrderNumber(string orderNumber);

		DataSet GetCustomerOrders();

		DataSet GetQuotes();

		DataSet GetDeliveryNotes();

		DataSet GetCustomerOrders(int customerID);

		DataSet GetQuotes(int customerID);

		DataSet GetDeliveryNotes(int customerID);

		DataSet GetCustomerOrders(DateTime from, DateTime to);

		DataSet GetQuotes(DateTime from, DateTime to);

		DataSet GetDeliveryNotes(DateTime from, DateTime to);

		DataSet GetCustomerOrders(int customerID, DateTime from, DateTime to);

		DataSet GetCustomerOrders(int[] customersID, DateTime from, DateTime to);

		DataSet GetQuotes(int customerID, DateTime from, DateTime to);

		DataSet GetDeliveryNotes(int customerID, DateTime from, DateTime to);

		bool DeleteCustomerOrder(int orderID);

		CustomerOrderData GetCustomerOrderByID(int orderID);

		bool ActivateOrder(int orderID, bool activate);

		DataSet GetItemsByOrderID(int orderID);

		DataSet[] GetItemsByOrderID(int[] ordersID);

		DataSet GetActiveOrdersByCustomer(int customerID);

		DataSet GetItemByNumber(string number);

		DataSet GetDeliveryNoteByNumber(string number);

		DataSet GetQuoteByNumber(string number);

		bool ChangeOrderStatus(int id, OrderStatusEnum orderStatus);

		bool ChangeOrderStatus(int[] ordersID, OrderStatusEnum orderStatus);

		DataSet GetCustomerOpenOrders();

		DataSet GetCustomerOpenOrders(int customerID);

		DataSet GetCustomerOpenOrders(int customerID, DateTime from, DateTime to);

		DataSet GetCustomerOpenOrders(DateTime from, DateTime to);

		DataSet GetCustomerPendingOrders(int customerID);

		DataSet GetCustomerShippedOrders(int customerID);

		DataSet GetCustomerCanceledOrders(int customerID);

		bool ExistDeliveryNoteNumber(string deliveryNoteNumber);

		bool ExistQuoteNumber(string quoteNumber);

		CustomerOrderData[] GetCustomerDeliveryNotesByID(int[] deliveryNotesID);

		CustomerOrderData[] GetCustomerQuotesByID(int[] quotesID);

		CustomerOrderData[] GetCustomerOrdersByID(int[] ordersID);

		DataSet GetCustomerOpenOrdersByJob(int[] jobsID, DateTime from, DateTime to);

		DataSet GetCustomerOpenOrdersByJob(int[] customersID, int[] jobsID, DateTime from, DateTime to);

		DataSet GetCustomerQuotesByJob(int[] jobsID, DateTime from, DateTime to);

		DataSet GetCustomerQuotesByJob(int[] customersID, int[] jobsID, DateTime from, DateTime to);

		bool CreateCustomerOrderBatch(DataSet listData);

		bool ContainsUnshipedItems(int orderID);

		DataSet GetItemByCriteria(string number, int[] customersID, DateTime from, DateTime to, decimal amount, AmountCriteria amountCriteria, string reference, string description);

		DataSet GetItemDeliveryNoteByCriteria(string number, int[] customersID, DateTime from, DateTime to, decimal amount, AmountCriteria amountCriteria, string reference, string description);

		DataSet GetItemQuoteByCriteria(string number, int[] customersID, DateTime from, DateTime to, decimal amount, AmountCriteria amountCriteria, string reference, string description);

		DataSet GetCustomerOpenOrders(int[] customersID, DateTime from, DateTime to);

		DataSet GetCustomerOpenOrders(int[] customersID, int[] itemsID, DateTime from, DateTime to);

		DataSet GetOrdersByFields(int[] customersID, OrderTypes orderType, DateTime from, DateTime to, params string[] columns);

		DataSet GetCustomerOrders(OrderTypes[] orderType, int[] customersID, DateTime from, DateTime to);
	}
}
