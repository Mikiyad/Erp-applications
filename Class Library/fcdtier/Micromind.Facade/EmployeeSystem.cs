using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Micromind.Facade
{
	public sealed class EmployeeSystem : MarshalByRefObject, IEmployeeSystem, IDisposable
	{
		private Config config;

		public EmployeeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployee(EmployeeData data)
		{
			return new Employees(config).InsertUpdateEmployee(data, isUpdate: false);
		}

		public bool UpdateEmployee(EmployeeData data)
		{
			return UpdateEmployee(data, checkConcurrency: false);
		}

		public bool UpdateEmployee(EmployeeData data, bool checkConcurrency)
		{
			return new Employees(config).InsertUpdateEmployee(data, isUpdate: true);
		}

		public bool UpdateEmployeeSalaryDetails(DataSet data, bool isRevised)
		{
			return new Employees(config).UpdateEmployeeSalaryDetails(data, isRevised);
		}

		public DataSet GetEmployeeSalaryDetailsByEmployeeID(string employeeID)
		{
			return new Employees(config).GetEmployeeSalaryDetailsByEmployeeID(employeeID);
		}

		public EmployeeData GetEmployee()
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployee();
			}
		}

		public bool DeleteEmployee(string groupID)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.DeleteEmployee(groupID);
			}
		}

		public EmployeeData GetEmployeeByID(string id)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeByID(id);
			}
		}

		public DataSet GetEmployeeByFields(params string[] columns)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeByFields(columns);
			}
		}

		public DataSet GetEmployeeByFields(string[] ids, params string[] columns)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeByFields(ids, columns);
			}
		}

		public DataSet GetEmployeeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEmployeeList()
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeList();
			}
		}

		public DataSet GetEmployeeList(bool includePhoto)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeList(includePhoto);
			}
		}

		public DataSet GetEmployeeComboList()
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeComboList();
			}
		}

		public DataSet GetEmployeeFilterComboList()
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeFilterComboList();
			}
		}

		public DataSet GetEmployeeBriefInfo(string employeeID)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeBriefInfo(employeeID);
			}
		}

		public DataSet GetEmployeeBriefInfoAbsconding(string employeeID)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeBriefInfoAbsconding(employeeID);
			}
		}

		public DataSet GetEmployeeCancellationInfo(string employeeID, string activityID)
		{
			using (new Employees(config))
			{
				return null;
			}
		}

		public DataSet GetEmployeeBalanceSummary(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showZeroBalance, DateTime to, string EmployeeIDs)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeBalanceSummary(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, showZeroBalance, to, EmployeeIDs);
			}
		}

		public DataSet GetEmployeeBalanceDetailReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showZeroBalance, string EmployeeIDs)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeBalanceDetailReport(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, from, to, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, showZeroBalance, EmployeeIDs);
			}
		}

		public DataSet GetEmployeeListReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeListReport(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, showInactive, EmployeeIDs);
			}
		}

		public DataSet GetEmployeeList(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeList(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, showInactive, EmployeeIDs);
			}
		}

		public DataSet GetEmployeeProfileReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeProfileReport(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, showInactive);
			}
		}

		public DataSet GetEmployeeProfileReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeProfileReport(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, showInactive, EmployeeIDs);
			}
		}

		public DataSet GetEmployeeActivityReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeActivityReport(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, showInactive, EmployeeIDs);
			}
		}

		public DataSet GetEmployeeLeaveReport(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromLeave, string toLeave, LeaveApprovalType approvalType)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeLeaveReport(from, to, fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromLeave, toLeave, approvalType);
			}
		}

		public DataSet GetEmployeeLeaveReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string fromLeave, string toLeave, LeaveApprovalType approvalType, string EmployeeIDs)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeLeaveReport(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, from, to, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, fromLeave, toLeave, approvalType, EmployeeIDs);
			}
		}

		public DataSet GetEmployeeGraduityEligibilityReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, DateTime asOfDate, string EmployeeIDs)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeGraduityEligibilityReport(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, asOfDate, EmployeeIDs);
			}
		}

		public DataSet GetEmployeeHistoryReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, DateTime asOfDate, string EmployeeIDs)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeHistoryReport(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, asOfDate, EmployeeIDs);
			}
		}

		public DataSet GetEmployeeSalaryReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, int periodYear, int periodMonth, string EmployeeIDs)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeSalaryReport(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, from, to, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, periodYear, periodMonth, EmployeeIDs);
			}
		}

		public DataSet GetEmployeeLeaveStatusReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string fromLeave, string toLeave, string EmployeeIDs)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeLeaveStatusReport(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, from, to, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, fromLeave, toLeave, EmployeeIDs);
			}
		}

		public DataSet GetEmployeeAnnualDueReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, object BasedOn, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string EmployeeIDs)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeAnnualDueReport(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, from, to, BasedOn, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, EmployeeIDs);
			}
		}

		public DataSet GetEmployeeLeaveInfo(string employeeID)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeLeaveInfo(employeeID);
			}
		}

		public DataSet GetEmployeeSalarySlipReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, int month, int year)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeSalarySlipReport(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, month, year);
			}
		}

		public DataSet GetEmployeeSalarySlipReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, int month, int year, string EmployeeIDs)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeSalarySlipReport(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, month, year, EmployeeIDs);
			}
		}

		public DataSet GetEmployeeSalarySlipReportWeb(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, int month, int year, string EmployeeIDs)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeSalarySlipReportWeb(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, month, year, EmployeeIDs);
			}
		}

		public byte[] GetEmployeeThumbnailImage(string employeeID)
		{
			return GetEmployeeThumbnailImage(employeeID, 128, 128);
		}

		public byte[] GetEmployeeThumbnailImage(string employeeID, int width, int height)
		{
			using (Employees employees = new Employees(config))
			{
				byte[] employeeThumbnailImage = employees.GetEmployeeThumbnailImage(employeeID);
				if (employeeThumbnailImage == null)
				{
					return null;
				}
				return CreateThumbnail(employeeThumbnailImage, width, height);
			}
		}

		private byte[] CreateThumbnail(byte[] image, int width, int height)
		{
			if (image == null)
			{
				return null;
			}
			ImageCodecInfo encoder = GetEncoder(ImageFormat.Jpeg);
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(image, 0, image.Length);
			Bitmap bitmap = new Bitmap(memoryStream);
			int num = width;
			int num2 = height;
			int width2 = bitmap.Width;
			int height2 = bitmap.Height;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			num4 = (float)num / (float)width2;
			num5 = (float)num2 / (float)height2;
			num3 = ((!(num5 < num4)) ? num4 : num5);
			checked
			{
				num = (int)((float)width2 * num3);
				num2 = (int)((float)height2 * num3);
				Image thumbnailImage = bitmap.GetThumbnailImage(num, num2, ThumbnailImageAbort, IntPtr.Zero);
				Encoder quality = Encoder.Quality;
				EncoderParameters encoderParameters = new EncoderParameters(1);
				EncoderParameter encoderParameter = new EncoderParameter(quality, 50L);
				encoderParameters.Param[0] = encoderParameter;
				memoryStream = new MemoryStream();
				thumbnailImage.Save(memoryStream, encoder, encoderParameters);
				return memoryStream.ToArray();
			}
		}

		private static bool ThumbnailImageAbort()
		{
			return true;
		}

		private ImageCodecInfo GetEncoder(ImageFormat format)
		{
			ImageCodecInfo[] imageDecoders = ImageCodecInfo.GetImageDecoders();
			foreach (ImageCodecInfo imageCodecInfo in imageDecoders)
			{
				if (imageCodecInfo.FormatID == format.Guid)
				{
					return imageCodecInfo;
				}
			}
			return null;
		}

		public bool AddEmployeePhoto(string employeeID, byte[] pictureByte)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.AddEmployeePhoto(employeeID, pictureByte);
			}
		}

		public bool RemoveEmployeePhoto(string employeeID)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.RemoveEmployeePhoto(employeeID);
			}
		}

		public DataSet GetEmployeeFinalSettlement(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, DateTime asOfDate, string EmployeeIDs)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEmployeeFinalSettlement(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, asOfDate, EmployeeIDs);
			}
		}

		public DataSet GetEmployeeSnapBalance(string EmployeeID)
		{
			return new EmployeeJournal(config).GetEmployeeSnapBalance(EmployeeID);
		}

		public DataSet GetEventEmployeeList()
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetEventEmployeeList();
			}
		}

		public DataSet GetActiveEmployeeList()
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetActiveEmployeeList();
			}
		}

		public bool IsEmployeeSettled(string employeeID)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.IsEmployeeSettled(employeeID);
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new Employees(config).GetList(from, to, showVoid);
		}

		public DataSet GetHRLetterReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, int month, int year, string EmployeeIDs, string strGroupBy)
		{
			using (Employees employees = new Employees(config))
			{
				return employees.GetHRLetterReport(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, month, year, EmployeeIDs, strGroupBy);
			}
		}
	}
}
