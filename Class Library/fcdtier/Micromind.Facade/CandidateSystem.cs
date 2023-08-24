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
	public sealed class CandidateSystem : MarshalByRefObject, ICandidateSystem, IDisposable
	{
		private Config config;

		public CandidateSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCandidate(CandidateData data)
		{
			return new Candidates(config).InsertUpdateCandidate(data, isUpdate: false);
		}

		public bool UpdateCandidate(CandidateData data)
		{
			return UpdateCandidate(data, checkConcurrency: false);
		}

		public bool UpdateCandidate(CandidateData data, bool checkConcurrency)
		{
			return new Candidates(config).InsertUpdateCandidate(data, isUpdate: true);
		}

		public bool UpdateCandidateSalaryDetails(DataSet data)
		{
			return false;
		}

		public DataSet GetCandidateSalaryDetailsByCandidateID(string candidateID)
		{
			return new Candidates(config).GetCandidateSalaryDetailsByCandidateID(candidateID);
		}

		public CandidateData GetCandidate()
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetCandidate();
			}
		}

		public bool DeleteCandidate(string groupID)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.DeleteCandidate(groupID);
			}
		}

		public bool CancelCandidate(CandidateData data)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.CancelCandidate(data);
			}
		}

		public CandidateData GetCandidateByID(string id)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetCandidateByID(id);
			}
		}

		public DataSet GetCandidateByFields(params string[] columns)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetCandidateByFields(columns);
			}
		}

		public DataSet GetCandidateByFields(string[] ids, params string[] columns)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetCandidateByFields(ids, columns);
			}
		}

		public DataSet GetCandidateByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetCandidateByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCandidateList()
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetCandidateList();
			}
		}

		public DataSet GetAppointmentList()
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetAppointmentList();
			}
		}

		public DataSet GetCandidateComboList()
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetCandidateComboList();
			}
		}

		public DataSet GetCandidateBriefInfo(string candidateID)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetCandidateBriefInfo(candidateID);
			}
		}

		public DataSet GetCandidateBalanceSummary(string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showZeroBalance)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetCandidateBalanceSummary(fromCandidate, toCandidate, fromDepartment, toDepartment, fromLocation, toLocation, showZeroBalance);
			}
		}

		public DataSet GetCandidateBalanceDetailReport(DateTime from, DateTime to, string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showZeroBalance)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetCandidateBalanceDetailReport(from, to, fromCandidate, toCandidate, fromDepartment, toDepartment, fromLocation, toLocation, showZeroBalance);
			}
		}

		public DataSet GetCandidateListReport(string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetCandidateListReport(fromCandidate, toCandidate, fromDepartment, toDepartment, fromLocation, toLocation, showInactive);
			}
		}

		public DataSet GetCandidateProfileReport(string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetCandidateProfileReport(fromCandidate, toCandidate, fromDepartment, toDepartment, fromLocation, toLocation, showInactive);
			}
		}

		public DataSet GetCandidateAppointmentDetails(string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetCandidateAppointmentDetails(fromCandidate, toCandidate, fromDepartment, toDepartment, fromLocation, toLocation, showInactive);
			}
		}

		public DataSet GetCandidateActivityReport(string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetCandidateActivityReport(fromCandidate, toCandidate, fromDepartment, toDepartment, fromLocation, toLocation, showInactive);
			}
		}

		public DataSet GetCandidateLeaveReport(DateTime from, DateTime to, string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromLeave, string toLeave, LeaveApprovalType approvalType)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetCandidateLeaveReport(from, to, fromCandidate, toCandidate, fromDepartment, toDepartment, fromLocation, toLocation, fromLeave, toLeave, approvalType);
			}
		}

		public bool AddCandidatePhoto(string candidateID, byte[] image)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.AddCandidatePhoto(candidateID, image);
			}
		}

		public bool RemoveCandidatePhoto(string candidateID)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.RemoveCandidatePhoto(candidateID);
			}
		}

		public byte[] GetCandidateThumbnailImage(string candidateID)
		{
			return GetCandidateThumbnailImage(candidateID, 128, 128);
		}

		public byte[] GetCandidateThumbnailImage(string candidateID, int width, int height)
		{
			using (Candidates candidates = new Candidates(config))
			{
				MemoryStream candidateThumbnailImage = candidates.GetCandidateThumbnailImage(candidateID);
				if (candidateThumbnailImage == null)
				{
					return null;
				}
				return CreateThumbnail(candidateThumbnailImage, width, height);
			}
		}

		private byte[] CreateThumbnail(MemoryStream stream, int width, int height)
		{
			if (stream == null)
			{
				return null;
			}
			ImageCodecInfo encoder = GetEncoder(ImageFormat.Jpeg);
			Bitmap bitmap = new Bitmap(stream);
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
				stream = new MemoryStream();
				thumbnailImage.Save(stream, encoder, encoderParameters);
				return stream.ToArray();
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

		public DataSet GetAgentComboList()
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetAgentComboList();
			}
		}

		public string GetNextSequenceNumber(string tableName, string fieldName)
		{
			using (Candidates candidates = new Candidates(config))
			{
				return candidates.GetNextSequenceNumber(tableName, fieldName);
			}
		}

		public bool IsEmployee(string employeeNo)
		{
			return new Candidates(config).IsEmployee(employeeNo);
		}
	}
}
