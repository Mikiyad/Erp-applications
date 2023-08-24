using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Micromind.ClientUI.WindowsForms.OCR.Lib
{
	public class TNSOCR
	{
		public const int ERROR_FIRST = 1879048192;

		public const int ERROR_FILENOTFOUND = 1879048193;

		public const int ERROR_LOADFILE = 1879048194;

		public const int ERROR_SAVEFILE = 1879048195;

		public const int ERROR_MISSEDIMGLOADER = 1879048196;

		public const int ERROR_OPTIONNOTFOUND = 1879048197;

		public const int ERROR_NOBLOCKS = 1879048198;

		public const int ERROR_BLOCKNOTFOUND = 1879048199;

		public const int ERROR_INVALIDINDEX = 1879048200;

		public const int ERROR_INVALIDPARAMETER = 1879048201;

		public const int ERROR_FAILED = 1879048202;

		public const int ERROR_INVALIDBLOCKTYPE = 1879048203;

		public const int ERROR_EMPTYTEXT = 1879048205;

		public const int ERROR_LOADINGDICTIONARY = 1879048206;

		public const int ERROR_LOADCHARBASE = 1879048207;

		public const int ERROR_NOMEMORY = 1879048208;

		public const int ERROR_CANNOTLOADGS = 1879048209;

		public const int ERROR_CANNOTPROCESSPDF = 1879048210;

		public const int ERROR_NOIMAGE = 1879048211;

		public const int ERROR_MISSEDSTEP = 1879048212;

		public const int ERROR_OUTOFIMAGE = 1879048213;

		public const int ERROR_EXCEPTION = 1879048214;

		public const int ERROR_NOTALLOWED = 1879048215;

		public const int ERROR_NODEFAULTDEVICE = 1879048216;

		public const int ERROR_NOTAPPLICABLE = 1879048217;

		public const int ERROR_MISSEDBARCODEDLL = 1879048218;

		public const int ERROR_PENDING = 1879048219;

		public const int ERROR_OPERATIONCANCELLED = 1879048220;

		public const int ERROR_TOOMANYLANGUAGES = 1879048221;

		public const int ERROR_OPERATIONTIMEOUT = 1879048222;

		public const int ERROR_LOAD_ASIAN_MODULE = 1879048223;

		public const int ERROR_LOAD_ASIAN_LANG = 1879048224;

		public const int ERROR_INVALIDOBJECT = 1879113728;

		public const int ERROR_TOOMANYOBJECTS = 1879113729;

		public const int ERROR_DLLNOTLOADED = 1879113730;

		public const int ERROR_DEMO = 1879113731;

		public const int BT_DEFAULT = 0;

		public const int BT_OCRTEXT = 1;

		public const int BT_ICRDIGIT = 2;

		public const int BT_CLEAR = 3;

		public const int BT_PICTURE = 4;

		public const int BT_ZONING = 5;

		public const int BT_OCRDIGIT = 6;

		public const int BT_BARCODE = 7;

		public const int BT_TABLE = 8;

		public const int BT_MRZ = 9;

		public const int BMP_24BIT = 0;

		public const int BMP_8BIT = 1;

		public const int BMP_1BIT = 2;

		public const int BMP_32BIT = 3;

		public const int BMP_BOTTOMTOP = 256;

		public const int FMT_EDITCOPY = 0;

		public const int FMT_EXACTCOPY = 1;

		public const int OCRSTEP_FIRST = 0;

		public const int OCRSTEP_PREFILTERS = 16;

		public const int OCRSTEP_BINARIZE = 32;

		public const int OCRSTEP_POSTFILTERS = 80;

		public const int OCRSTEP_REMOVELINES = 96;

		public const int OCRSTEP_ZONING = 112;

		public const int OCRSTEP_OCR = 128;

		public const int OCRSTEP_LAST = 255;

		public const int OCRFLAG_NONE = 0;

		public const int OCRFLAG_THREAD = 1;

		public const int OCRFLAG_GETRESULT = 2;

		public const int OCRFLAG_GETPROGRESS = 3;

		public const int OCRFLAG_CANCEL = 4;

		public const int DRAW_NORMAL = 0;

		public const int DRAW_BINARY = 1;

		public const int DRAW_GETBPP = 256;

		public const int BLK_INVERSE_GET = -1;

		public const int BLK_INVERSE_SET0 = 0;

		public const int BLK_INVERSE_SET1 = 1;

		public const int BLK_INVERSE_DETECT = 256;

		public const int BLK_ROTATE_GET = -1;

		public const int BLK_ROTATE_NONE = 0;

		public const int BLK_ROTATE_90 = 1;

		public const int BLK_ROTATE_180 = 2;

		public const int BLK_ROTATE_270 = 3;

		public const int BLK_ROTATE_ANGLE = 1048576;

		public const int BLK_ROTATE_DETECT = 256;

		public const int BLK_MIRROR_GET = -1;

		public const int BLK_MIRROR_NONE = 0;

		public const int BLK_MIRROR_H = 1;

		public const int BLK_MIRROR_V = 2;

		public const int SVR_FORMAT_PDF = 1;

		public const int SVR_FORMAT_RTF = 2;

		public const int SVR_FORMAT_TXT_ASCII = 3;

		public const int SVR_FORMAT_TXT_UNICODE = 4;

		public const int SVR_FORMAT_XML = 5;

		public const int SVR_FORMAT_PDFA = 6;

		public const int SCAN_GETDEFAULTDEVICE = 1;

		public const int SCAN_SETDEFAULTDEVICE = 256;

		public const int SCAN_NOUI = 1;

		public const int SCAN_SOURCEADF = 2;

		public const int SCAN_SOURCEAUTO = 4;

		public const int SCAN_DONTCLOSEDS = 8;

		public const int SCAN_FILE_SEPARATE = 16;

		public const int FONT_STYLE_UNDERLINED = 1;

		public const int FONT_STYLE_STRIKED = 2;

		public const int FONT_STYLE_BOLD = 4;

		public const int FONT_STYLE_ITALIC = 8;

		public const int IMG_PROP_DPIX = 1;

		public const int IMG_PROP_DPIY = 2;

		public const int IMG_PROP_BPP = 3;

		public const int IMG_PROP_WIDTH = 4;

		public const int IMG_PROP_HEIGHT = 5;

		public const int IMG_PROP_INVERTED = 6;

		public const int IMG_PROP_SKEW = 7;

		public const int IMG_PROP_SCALE = 8;

		public const int IMG_PROP_PAGEINDEX = 9;

		public const int REGEX_SET = 0;

		public const int REGEX_CLEAR = 1;

		public const int REGEX_CLEAR_ALL = 2;

		public const int REGEX_DISABLE_DICT = 4;

		public const int REGEX_CHECK = 8;

		public const int INFO_PDF_AUTHOR = 1;

		public const int INFO_PDF_CREATOR = 2;

		public const int INFO_PDF_PRODUCER = 3;

		public const int INFO_PDF_TITLE = 4;

		public const int INFO_PDF_SUBJECT = 5;

		public const int INFO_PDF_KEYWORDS = 6;

		public const int BARCODE_TYPE_EAN8 = 1;

		public const int BARCODE_TYPE_UPCE = 2;

		public const int BARCODE_TYPE_ISBN10 = 3;

		public const int BARCODE_TYPE_UPCA = 4;

		public const int BARCODE_TYPE_EAN13 = 5;

		public const int BARCODE_TYPE_ISBN13 = 6;

		public const int BARCODE_TYPE_ZBAR_I25 = 7;

		public const int BARCODE_TYPE_CODE39 = 8;

		public const int BARCODE_TYPE_QRCODE = 9;

		public const int BARCODE_TYPE_CODE128 = 10;

		public const int BARCODE_TYPE_MASK_EAN8 = 1;

		public const int BARCODE_TYPE_MASK_UPCE = 2;

		public const int BARCODE_TYPE_MASK_ISBN10 = 4;

		public const int BARCODE_TYPE_MASK_UPCA = 8;

		public const int BARCODE_TYPE_MASK_EAN13 = 16;

		public const int BARCODE_TYPE_MASK_ISBN13 = 32;

		public const int BARCODE_TYPE_MASK_ZBAR_I25 = 64;

		public const int BARCODE_TYPE_MASK_CODE39 = 128;

		public const int BARCODE_TYPE_MASK_QRCODE = 256;

		public const int BARCODE_TYPE_MASK_CODE128 = 512;

		public const int IMG_FORMAT_BMP = 0;

		public const int IMG_FORMAT_JPEG = 2;

		public const int IMG_FORMAT_PNG = 13;

		public const int IMG_FORMAT_TIFF = 18;

		public const int IMG_FORMAT_FLAG_BINARIZED = 256;

		public const string LIBNAME = "\\NSOCR.dll";

		[DllImport("\\NSOCR.dll")]
		public static extern int Engine_Initialize();

		[DllImport("\\NSOCR.dll")]
		public static extern int Engine_InitializeAdvanced(out int CfgObj, out int OcrObj, out int ImgObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Engine_Uninitialize();

		[DllImport("\\NSOCR.dll")]
		public static extern int Engine_SetDataDirectory([MarshalAs(UnmanagedType.LPWStr)] string DirectoryPath);

		[DllImport("\\NSOCR.dll")]
		public static extern int Engine_GetVersion([MarshalAs(UnmanagedType.LPWStr)] StringBuilder OptionValue);

		[DllImport("\\NSOCR.dll")]
		public static extern int Engine_SetLicenseKey([MarshalAs(UnmanagedType.LPWStr)] string LicenseKey);

		[DllImport("\\NSOCR.dll")]
		public static extern int Cfg_Create(out int CfgObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Cfg_Destroy(int CfgObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Cfg_LoadOptions(int CfgObj, [MarshalAs(UnmanagedType.LPWStr)] string FileName);

		[DllImport("\\NSOCR.dll")]
		public static extern int Cfg_SaveOptions(int CfgObj, [MarshalAs(UnmanagedType.LPWStr)] string FileName);

		[DllImport("\\NSOCR.dll")]
		public static extern int Cfg_LoadOptionsFromString(int CfgObj, [MarshalAs(UnmanagedType.LPWStr)] string XMLString);

		[DllImport("\\NSOCR.dll")]
		public static extern int Cfg_SaveOptionsToString(int CfgObj, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder XMLString, int MaxLen);

		[DllImport("\\NSOCR.dll")]
		public static extern int Cfg_GetOption(int CfgObj, int BlockType, [MarshalAs(UnmanagedType.LPWStr)] string OptionPath, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder OptionValue, int MaxLen);

		[DllImport("\\NSOCR.dll")]
		public static extern int Cfg_SetOption(int CfgObj, int BlockType, [MarshalAs(UnmanagedType.LPWStr)] string OptionPath, [MarshalAs(UnmanagedType.LPWStr)] string OptionValue);

		[DllImport("\\NSOCR.dll")]
		public static extern int Cfg_DeleteOption(int CfgObj, int BlockType, [MarshalAs(UnmanagedType.LPWStr)] string OptionPath);

		[DllImport("\\NSOCR.dll")]
		public static extern int Ocr_Create(int CfgObj, out int OcrObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Ocr_Destroy(int OcrObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Ocr_Destroy(int ImgObj, int SvrObj, int PageIndexStart, int PageIndexEnd, int OcrObjCnt, int Flags);

		[DllImport("\\NSOCR.dll")]
		public static extern int Ocr_ProcessPages(int ImgObj, int SvrObj, int PageIndexStart, int PageIndexEnd, int OcrObjCnt, int Flags);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_Create(int OcrObj, out int ImgObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_Destroy(int ImgObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_LoadFile(int ImgObj, [MarshalAs(UnmanagedType.LPWStr)] string FileName);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_LoadFromMemory(int ImgObj, IntPtr Bytes, int DataSize);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_LoadBmpData(int ImgObj, IntPtr Bytes, int Width, int Height, int Flags, int Stride);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_Unload(int ImgObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_GetPageCount(int ImgObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_SetPage(int ImgObj, int PageIndex);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_GetSize(int ImgObj, out int Width, out int Height);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_DrawToDC(int ImgObj, int HandleDC, int X, int Y, ref int Width, ref int Height, int Flags);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_DeleteAllBlocks(int ImgObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_AddBlock(int ImgObj, int Xpos, int Ypos, int Width, int Height, out int BlkObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_DeleteBlock(int ImgObj, int BlkObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_GetBlockCnt(int ImgObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_GetBlock(int ImgObj, int BlockIndex, out int BlkObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_GetImgText(int ImgObj, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder TextStr, int MaxLen, int Flags);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_GetBmpData(int ImgObj, IntPtr Bits, ref int Width, ref int Height, int Flags);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_OCR(int ImgObj, int FirstStep, int LastStep, int Flags);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_SaveBlocks(int ImgObj, [MarshalAs(UnmanagedType.LPWStr)] string FileName);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_LoadBlocks(int ImgObj, [MarshalAs(UnmanagedType.LPWStr)] string FileName);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_GetSkewAngle(int ImgObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_GetPixLineCnt(int ImgObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_GetPixLine(int ImgObj, int LineInd, out int X1pos, out int Y1pos, out int X2pos, out int Y2pos, out int Width);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_GetScaleFactor(int ImgObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_CalcPointPosition(int ImgObj, ref int Xpos, ref int Ypos, int Mode);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_CopyCurrentPage(int ImgObjSrc, int ImgObjDst, int Flags);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_GetProperty(int ImgObj, int PropertyID);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_SaveToFile(int ImgObj, [MarshalAs(UnmanagedType.LPWStr)] string FileName, int Format, int Flags);

		[DllImport("\\NSOCR.dll")]
		public static extern int Img_SaveToMemory(int ImgObj, IntPtr Bytes, int BufferSize, int Format, int Flags);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetType(int BlkObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_SetType(int BlkObj, int BlockType);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetRect(int BlkObj, out int Xpos, out int Ypos, out int Width, out int Height);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetText(int BlkObj, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder TextStr, int MaxLen, int Flags);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetLineCnt(int BlkObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetLineText(int BlkObj, int LineIndex, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder TextStr, int MaxLen);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetWordCnt(int BlkObj, int LineIndex);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetWordText(int BlkObj, int LineIndex, int WordIndex, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder TextStr, int MaxLen);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_SetWordText(int BlkObj, int LineIndex, int WordIndex, [MarshalAs(UnmanagedType.LPWStr)] string TextStr);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetCharCnt(int BlkObj, int LineIndex, int WordIndex);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetCharRect(int BlkObj, int LineIndex, int WordIndex, int CharIndex, out int Xpos, out int Ypos, out int Width, out int Height);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetCharText(int BlkObj, int LineIndex, int WordIndex, int CharIndex, int ResultIndex, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder TextStr, int MaxLen);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetCharQual(int BlkObj, int LineIndex, int WordIndex, int CharIndex, int ResultIndex);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetTextRect(int BlkObj, int LineIndex, int WordIndex, out int Xpos, out int Ypos, out int Width, out int Height);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_Inversion(int BlkObj, int Inversion);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_Rotation(int BlkObj, int Rotation);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_Mirror(int BlkObj, int Mirror);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_IsWordInDictionary(int BlkObj, int LineIndex, int WordIndex);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_SetRect(int BlkObj, int Xpos, int Ypos, int Width, int Height);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetWordQual(int BlkObj, int LineIndex, int WordIndex);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetWordFontColor(int BlkObj, int LineIndex, int WordIndex);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetWordFontSize(int BlkObj, int LineIndex, int WordIndex);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetWordFontStyle(int BlkObj, int LineIndex, int WordIndex);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetBackgroundColor(int BlkObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_SetWordRegEx(int BlkObj, int LineIndex, int WordIndex, [MarshalAs(UnmanagedType.LPWStr)] string RegEx, int Flags);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetBarcodeCnt(int BlkObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetBarcodeText(int BlkObj, int BarcodeInd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder TextStr, int MaxLen);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetBarcodeRect(int BlkObj, int BarcodeInd, out int Xpos, out int Ypos, out int Width, out int Height);

		[DllImport("\\NSOCR.dll")]
		public static extern int Blk_GetBarcodeType(int BlkObj, int BarcodeInd);

		[DllImport("\\NSOCR.dll")]
		public static extern int Svr_Create(int CfgObj, int Format, out int SvrObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Svr_Destroy(int SvrObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Svr_NewDocument(int SvrObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Svr_AddPage(int SvrObj, int ImgObj, int Flags);

		[DllImport("\\NSOCR.dll")]
		public static extern int Svr_SaveToFile(int SvrObj, [MarshalAs(UnmanagedType.LPWStr)] string FileName);

		[DllImport("\\NSOCR.dll")]
		public static extern int Svr_SaveToMemory(int SvrObj, IntPtr Bytes, int BufferSize);

		[DllImport("\\NSOCR.dll")]
		public static extern int Svr_GetText(int SvrObj, int PageIndex, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder TextStr, int MaxLen);

		[DllImport("\\NSOCR.dll")]
		public static extern int Svr_SetDocumentInfo(int SvrObj, int InfoID, [MarshalAs(UnmanagedType.LPWStr)] string InfoStr);

		[DllImport("\\NSOCR.dll")]
		public static extern int Scan_Create(int CfgObj, out int ScanObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Scan_Destroy(int ScanObj);

		[DllImport("\\NSOCR.dll")]
		public static extern int Scan_Enumerate(int ScanObj, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder ScannerNames, int MaxLen, int Flags);

		[DllImport("\\NSOCR.dll")]
		public static extern int Scan_ScanToImg(int ScanObj, int ImgObj, int ScannerIndex, int Flags);

		[DllImport("\\NSOCR.dll")]
		public static extern int Scan_ScanToFile(int ScanObj, [MarshalAs(UnmanagedType.LPWStr)] string FileName, int ScannerIndex, int Flags);
	}
}
