using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Micromind.DataControls.QueryBuilder
{
	public class Parser
	{
		public enum eType
		{
			Comand = 1,
			Keyword,
			Function,
			Operator,
			Number,
			DataType,
			String,
			CommentL,
			CommentP,
			ParOpen,
			ParClose,
			LineBreak,
			Comma,
			Unknown,
			Invalid
		}

		public enum eCmd
		{
			Undefined,
			Alter,
			AndOr,
			As,
			Begin,
			Between,
			Case,
			Column,
			Compute,
			Create,
			Delete,
			Distributed,
			Exec,
			Else,
			End,
			For,
			From,
			Function,
			Group,
			Having,
			If,
			Insert,
			Into,
			Join,
			Left,
			On,
			Option,
			Order,
			Procedure,
			Returns,
			Right,
			Select,
			Set,
			Top,
			Transaction,
			Trigger,
			Union,
			Update,
			View,
			When,
			Where
		}

		public enum eIndent
		{
			Normal,
			Shift,
			SingleLF,
			DoubleLF
		}

		protected class ParseItem
		{
			protected Parser mi_Parser;

			protected eType me_Type = eType.Unknown;

			protected eCmd me_Cmd;

			protected eIndent me_Indent;

			protected eIndent me_Outdent;

			protected string ms_Text;

			protected ParseItem mi_Next;

			protected ParseItem mi_Prev;

			protected bool mb_Permanent;

			protected bool mb_AppendSpace = true;

			protected int ms32_RelCursor = -1;

			protected ArrayList mi_Contains;

			protected static Hashtable mi_Colors;

			public string Text
			{
				get
				{
					return ms_Text;
				}
				set
				{
					ms_Text = value.TrimStart(" ".ToCharArray());
					me_Type = eType.Unknown;
					me_Cmd = eCmd.Undefined;
					mb_AppendSpace = true;
					if (ms_Text.Length == 0)
					{
						throw new Exception("Invalid item text");
					}
					if (ms_Text.Length == 1)
					{
						switch (ms_Text[0])
						{
						case '(':
							me_Type = eType.ParOpen;
							return;
						case ')':
							me_Type = eType.ParClose;
							return;
						case ',':
							me_Type = eType.Comma;
							return;
						case '\n':
							me_Type = eType.LineBreak;
							mb_AppendSpace = false;
							return;
						}
					}
					if (ms_Text.StartsWith("/*"))
					{
						me_Type = eType.CommentP;
						return;
					}
					if (ms_Text.StartsWith("'") || ms_Text.StartsWith("N'"))
					{
						me_Type = eType.String;
						return;
					}
					ms_Text = ms_Text.Replace("\n", " ");
					if (ms_Text.StartsWith("--"))
					{
						if (!Defaults.CheckLineComment(ms_Text))
						{
							me_Type = eType.Invalid;
							return;
						}
						me_Type = eType.CommentL;
						mb_AppendSpace = false;
						return;
					}
					ms_Text = ms_Text.Trim();
					string s_Value = " " + ms_Text + " ";
					if (Functions.IndexOf(" alter backup begin break close commit continue create deallocate declare delete drop else end exec execute fetch go goto grant if insert load open print raiserror restore return revoke rollback save select set setuser shutdown update updatetext use waitfor while writetext ", s_Value, 0) >= 0)
					{
						me_Type = eType.Comand;
						ms_Text = ms_Text.ToUpper();
						switch (ms_Text)
						{
						case "ALTER":
							me_Cmd = eCmd.Alter;
							break;
						case "BEGIN":
							me_Cmd = eCmd.Begin;
							break;
						case "CREATE":
							me_Cmd = eCmd.Create;
							break;
						case "DELETE":
							me_Cmd = eCmd.Delete;
							break;
						case "EXEC":
							me_Cmd = eCmd.Exec;
							break;
						case "ELSE":
							me_Cmd = eCmd.Else;
							break;
						case "END":
							me_Cmd = eCmd.End;
							break;
						case "IF":
							me_Cmd = eCmd.If;
							break;
						case "INSERT":
							me_Cmd = eCmd.Insert;
							break;
						case "SELECT":
							me_Cmd = eCmd.Select;
							break;
						case "SET":
							me_Cmd = eCmd.Set;
							break;
						case "UPDATE":
							me_Cmd = eCmd.Update;
							break;
						}
						return;
					}
					if (Functions.IndexOf(" action add all and any append as asc authorization between browse bulk by cascade case check checkpoint clustered collate column committed compute confirm constraint contains containstable controlrow cross cube current current_date current_time cursor database dbcc default deny desc disk distinct distributed double dummy dump encryption errlvl errorexit escape except exists exit file fillfactor floppy for foreign forward_only freetext freetexttable from full function group grouping having holdlock identity identity_insert identitycol in index inner intersect into is isolation join key kill left level like lineno mirrorexit move national next no nocheck nonclustered nocount nolock not nounload null of off offsets on once only opendatasource openquery openrowset option or order outer over percent perm permanent pipe plan precision prepare primary privileges proc procedure processexit public read readtext read_only reconfigure recovery references repeatable replication restrict returns right rollup rowguidcol rule schema serializable some statistics stats table tape temp temporary then ties to top tran transaction trigger truncate tsequal uncommitted union unique values varying view when where with work ", s_Value, 0) >= 0)
					{
						me_Type = eType.Keyword;
						ms_Text = ms_Text.ToUpper();
						switch (ms_Text)
						{
						case "AND":
							me_Cmd = eCmd.AndOr;
							break;
						case "AS":
							me_Cmd = eCmd.As;
							break;
						case "BETWEEN":
							me_Cmd = eCmd.Between;
							break;
						case "CASE":
							me_Cmd = eCmd.Case;
							break;
						case "COLUMN":
							me_Cmd = eCmd.Column;
							break;
						case "COMPUTE":
							me_Cmd = eCmd.Compute;
							break;
						case "DISTRIBUTED":
							me_Cmd = eCmd.Distributed;
							break;
						case "FOR":
							me_Cmd = eCmd.For;
							break;
						case "FROM":
							me_Cmd = eCmd.From;
							break;
						case "FUNCTION":
							me_Cmd = eCmd.Function;
							break;
						case "GROUP":
							me_Cmd = eCmd.Group;
							break;
						case "HAVING":
							me_Cmd = eCmd.Having;
							break;
						case "INTO":
							me_Cmd = eCmd.Into;
							break;
						case "JOIN":
							me_Cmd = eCmd.Join;
							break;
						case "LEFT":
							me_Cmd = eCmd.Left;
							break;
						case "OPTION":
							me_Cmd = eCmd.Option;
							break;
						case "ON":
							me_Cmd = eCmd.On;
							break;
						case "OR":
							me_Cmd = eCmd.AndOr;
							break;
						case "ORDER":
							me_Cmd = eCmd.Order;
							break;
						case "PROC":
						case "PROCEDURE":
							me_Cmd = eCmd.Procedure;
							break;
						case "RETURNS":
							me_Cmd = eCmd.Returns;
							break;
						case "RIGHT":
							me_Cmd = eCmd.Right;
							break;
						case "TOP":
							me_Cmd = eCmd.Top;
							break;
						case "TRAN":
						case "TRANSACTION":
							me_Cmd = eCmd.Transaction;
							break;
						case "TRIGGER":
							me_Cmd = eCmd.Trigger;
							break;
						case "UNION":
							me_Cmd = eCmd.Union;
							break;
						case "VIEW":
							me_Cmd = eCmd.View;
							break;
						case "WHEN":
							me_Cmd = eCmd.When;
							break;
						case "WHERE":
							me_Cmd = eCmd.Where;
							break;
						}
						return;
					}
					if (Functions.IndexOf(" abs acos app_name ascii asin atan atn2 avg cast case ceiling charindex checksum checksum_agg coalesce collationproperty col_length col_name columnproperty convert count cos cot count_big current_timestamp current_user cursor_status databaseproperty databasepropertyex datalength dateadd datediff datename datepart day db_id db_name degrees difference exp file_id file_name filegroup_id filegroup_name filegroupproperty fileproperty floor formatmessage fulltextcatalogproperty fulltextserviceproperty getansinull getdate getutcdate host_id host_name ident_current ident_incr ident_seed index_col indexkey_property indexproperty is_member is_srvrolemember isdate isnull isnumeric len log log10 lower ltrim max min month newid nullif object_id object_name objectproperty parsename patindex permissions pi power quotename radians rand replace replicate reverse round rtrim scope_identity serverproperty session_user sessionproperty sign sin soundex space sql_variant_property sqrt stats_date stdev stdevp str stuff substring sum suser_sid suser_sname system_user tan textptr textvalid typeproperty unicode upper user user_id user_name var varp year ", s_Value, 0) >= 0)
					{
						me_Type = eType.Function;
						ms_Text = ms_Text.ToLower();
						return;
					}
					if (Functions.IndexOf(" nBinario nBoolean nDate nDecimal nFloat nGUID nLong nPrecio nStringBig nStringLong nStringMax nStringMid nStringShort nTexto  bigint binary bit char character datetime dec decimal float image int integer money nchar ntext numeric nvarchar real smalldatetime smallint smallmoney sql_variant sysname text timestamp tinyint uniqueidentifier varbinary varchar ", s_Value, 0) >= 0)
					{
						me_Type = eType.DataType;
						ms_Text = ms_Text.ToLower();
						return;
					}
					if (Functions.IndexOf(" + - * / % = != !> !< < > <> <= >= =* ", s_Value, 0) >= 0)
					{
						me_Type = eType.Operator;
						return;
					}
					bool flag = true;
					for (int i = 0; i < ms_Text.Length; i++)
					{
						if (ms_Text[i] < '0' || ms_Text[i] > '9')
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						me_Type = eType.Number;
					}
					else
					{
						me_Type = eType.Unknown;
					}
				}
			}

			public eType Type
			{
				get
				{
					return me_Type;
				}
				set
				{
					me_Type = value;
					if (me_Type == eType.Function)
					{
						ms_Text = ms_Text.ToLower();
					}
					if (me_Type == eType.Comand || me_Type == eType.Keyword)
					{
						ms_Text = ms_Text.ToUpper();
					}
				}
			}

			public eCmd Cmd => me_Cmd;

			public ParseItem Next => mi_Next;

			public ParseItem Prev => mi_Prev;

			public bool Permanent
			{
				get
				{
					return mb_Permanent;
				}
				set
				{
					mb_Permanent = value;
				}
			}

			public int CursorPos
			{
				get
				{
					return ms32_RelCursor;
				}
				set
				{
					ms32_RelCursor = value;
				}
			}

			public eIndent Indent
			{
				get
				{
					return me_Indent;
				}
				set
				{
					if (value > me_Indent)
					{
						me_Indent = value;
						if (value >= eIndent.SingleLF)
						{
							SetLinebreakBefore(this, 1);
						}
						if (value == eIndent.DoubleLF)
						{
							SetLinebreakAfter(this, 1);
						}
					}
				}
			}

			public eIndent Outdent
			{
				get
				{
					return me_Outdent;
				}
				set
				{
					if (value > me_Outdent)
					{
						me_Outdent = value;
						if (value >= eIndent.SingleLF)
						{
							SetLinebreakAfter(this, 1);
						}
						if (value == eIndent.DoubleLF)
						{
							SetLinebreakBefore(this, 1);
						}
					}
				}
			}

			public ParseItem(Parser i_Parser, string s_Text)
			{
				mi_Parser = i_Parser;
				if (mi_Colors == null)
				{
					mi_Colors = Defaults.DefineParserColors();
				}
				Text = s_Text;
			}

			public void Remove(bool b_IgnorePermanent)
			{
				if ((mb_Permanent && !b_IgnorePermanent) || (me_Type == eType.LineBreak && !mi_Parser.mb_ModifyLineBR) || (getType(this) == eType.LineBreak && Next == null) || (getType(Next) == eType.LineBreak && Next.Next == null))
				{
					return;
				}
				if (mi_Next != null)
				{
					mi_Next.mi_Prev = mi_Prev;
				}
				if (mi_Prev != null)
				{
					mi_Prev.mi_Next = mi_Next;
				}
				if (CursorPos < 0)
				{
					return;
				}
				if (me_Type == eType.LineBreak)
				{
					if (getType(mi_Prev) == eType.LineBreak)
					{
						mi_Prev.CursorPos = CursorPos;
						Functions.PrintDebug("Remove()  set CursorPos={0} to prev LBR  {1} <{2}> {3}", CursorPos, getDebug(Prev), getDebug(this), getDebug(Next), typeof(Parser));
						return;
					}
					if (getType(mi_Next) == eType.LineBreak)
					{
						mi_Next.CursorPos = CursorPos;
						Functions.PrintDebug("Remove()  set CursorPos={0} to next LBR  {1} <{2}> {3}", CursorPos, getDebug(Prev), getDebug(this), getDebug(Next), typeof(Parser));
						return;
					}
					if (CursorPos == 1 && mi_Next != null)
					{
						mi_Next.CursorPos = 0;
						Functions.PrintDebug("Remove()  set CursorPos=0 to next item  {0} <{1}> {2}", getDebug(Prev), getDebug(this), getDebug(Next), typeof(Parser));
						return;
					}
				}
				if (mi_Prev != null)
				{
					mi_Prev.CursorPos = mi_Prev.Text.Length + (mb_AppendSpace ? 1 : 0);
					Functions.PrintDebug("Remove()  set CursorPos={0} to prev item  {1} <{2}> {3}", mi_Prev.CursorPos, getDebug(Prev), getDebug(this), getDebug(Next), typeof(Parser));
				}
			}

			public void InsertAfter(ParseItem i_Item)
			{
				i_Item.mi_Prev = this;
				i_Item.mi_Next = mi_Next;
				if (mi_Next != null)
				{
					mi_Next.mi_Prev = i_Item;
				}
				mi_Next = i_Item;
			}

			public void InsertBefore(ParseItem i_Item)
			{
				i_Item.mi_Next = this;
				i_Item.mi_Prev = mi_Prev;
				if (mi_Prev != null)
				{
					mi_Prev.mi_Next = i_Item;
				}
				mi_Prev = i_Item;
			}

			public static void SetLinebreakBefore(ParseItem i_Item, int s32_Count)
			{
				if (i_Item == null || i_Item.Prev == null || !i_Item.mi_Parser.mb_ModifyLineBR)
				{
					return;
				}
				for (int i = 0; i < s32_Count; i++)
				{
					if (getType(i_Item.Prev) != eType.LineBreak)
					{
						i_Item.InsertBefore(new ParseItem(i_Item.mi_Parser, "\n"));
					}
					i_Item = i_Item.Prev;
					i_Item.Permanent = true;
				}
			}

			public static void SetLinebreakAfter(ParseItem i_Item, int s32_Count)
			{
				if (i_Item == null || i_Item.Next == null || !i_Item.mi_Parser.mb_ModifyLineBR)
				{
					return;
				}
				for (int i = 0; i < s32_Count; i++)
				{
					if (getType(i_Item.Next) != eType.LineBreak)
					{
						i_Item.InsertAfter(new ParseItem(i_Item.mi_Parser, "\n"));
					}
					i_Item = i_Item.Next;
					i_Item.Permanent = true;
				}
			}

			public ParseItem FindBeforeLF()
			{
				ParseItem prev = Prev;
				while (getType(prev) == eType.LineBreak)
				{
					prev = prev.Prev;
				}
				return prev;
			}

			public ParseItem FindAfterLF()
			{
				ParseItem next = Next;
				while (getType(next) == eType.LineBreak)
				{
					next = next.Next;
				}
				return next;
			}

			public void GetText(RtfHtmlBuilder i_Builder)
			{
				if (me_Outdent != 0)
				{
					i_Builder.SelectionIndent--;
				}
				if (mi_Colors[me_Type] != null)
				{
					i_Builder.SelectionColor = (Color)mi_Colors[me_Type];
				}
				else
				{
					i_Builder.SelectionColor = Color.Black;
				}
				i_Builder.AppendText(ms_Text);
				if (mb_AppendSpace)
				{
					i_Builder.AppendText(" ");
				}
				if (me_Indent != 0)
				{
					i_Builder.SelectionIndent++;
				}
			}

			public void SetContains(object o_Obj)
			{
				if (mi_Contains == null)
				{
					mi_Contains = new ArrayList();
				}
				if (!mi_Contains.Contains(o_Obj))
				{
					mi_Contains.Add(o_Obj);
				}
			}

			public bool Contains(object o_Obj)
			{
				if (mi_Contains != null)
				{
					return mi_Contains.Contains(o_Obj);
				}
				return false;
			}
		}

		private const string COMMANDS = " alter backup begin break close commit continue create deallocate declare delete drop else end exec execute fetch go goto grant if insert load open print raiserror restore return revoke rollback save select set setuser shutdown update updatetext use waitfor while writetext ";

		private const string KEYWORDS = " action add all and any append as asc authorization between browse bulk by cascade case check checkpoint clustered collate column committed compute confirm constraint contains containstable controlrow cross cube current current_date current_time cursor database dbcc default deny desc disk distinct distributed double dummy dump encryption errlvl errorexit escape except exists exit file fillfactor floppy for foreign forward_only freetext freetexttable from full function group grouping having holdlock identity identity_insert identitycol in index inner intersect into is isolation join key kill left level like lineno mirrorexit move national next no nocheck nonclustered nocount nolock not nounload null of off offsets on once only opendatasource openquery openrowset option or order outer over percent perm permanent pipe plan precision prepare primary privileges proc procedure processexit public read readtext read_only reconfigure recovery references repeatable replication restrict returns right rollup rowguidcol rule schema serializable some statistics stats table tape temp temporary then ties to top tran transaction trigger truncate tsequal uncommitted union unique values varying view when where with work ";

		private const string FUNCTIONS = " abs acos app_name ascii asin atan atn2 avg cast case ceiling charindex checksum checksum_agg coalesce collationproperty col_length col_name columnproperty convert count cos cot count_big current_timestamp current_user cursor_status databaseproperty databasepropertyex datalength dateadd datediff datename datepart day db_id db_name degrees difference exp file_id file_name filegroup_id filegroup_name filegroupproperty fileproperty floor formatmessage fulltextcatalogproperty fulltextserviceproperty getansinull getdate getutcdate host_id host_name ident_current ident_incr ident_seed index_col indexkey_property indexproperty is_member is_srvrolemember isdate isnull isnumeric len log log10 lower ltrim max min month newid nullif object_id object_name objectproperty parsename patindex permissions pi power quotename radians rand replace replicate reverse round rtrim scope_identity serverproperty session_user sessionproperty sign sin soundex space sql_variant_property sqrt stats_date stdev stdevp str stuff substring sum suser_sid suser_sname system_user tan textptr textvalid typeproperty unicode upper user user_id user_name var varp year ";

		private const string DATATYPES = " nBinario nBoolean nDate nDecimal nFloat nGUID nLong nPrecio nStringBig nStringLong nStringMax nStringMid nStringShort nTexto  bigint binary bit char character datetime dec decimal float image int integer money nchar ntext numeric nvarchar real smalldatetime smallint smallmoney sql_variant sysname text timestamp tinyint uniqueidentifier varbinary varchar ";

		private const string OPERATORS = " + - * / % = != !> !< < > <> <= >= =* ";

		protected RtfHtmlBuilder mi_RtfHtmlBuilder = new RtfHtmlBuilder();

		protected ParseItem mi_FirstItem;

		protected int ms32_CursorPos;

		protected bool mb_CursorStored;

		protected bool mb_ModifyLineBR;

		protected bool mb_Abort;

		public string SQL => mi_RtfHtmlBuilder.GetText();

		public string HTML => mi_RtfHtmlBuilder.GetHtml();

		public void Abort()
		{
			mb_Abort = true;
		}

		public bool Parse(string s_SQL, bool b_ModifyLinebreaks, bool b_Append_LF)
		{
			int s32_CursorPos = 0;
			return Parse(s_SQL, b_ModifyLinebreaks, b_Append_LF, ref s32_CursorPos);
		}

		public bool Parse(string s_SQL, bool b_ModifyLinebreaks, bool b_Append_LF, ref int s32_CursorPos)
		{
			mb_Abort = false;
			mb_CursorStored = false;
			ms32_CursorPos = s32_CursorPos;
			mb_ModifyLineBR = b_ModifyLinebreaks;
			if (s_SQL == null)
			{
				s_SQL = "";
			}
			Functions.PrintDebug("-------------- Start Parsing ---------------", typeof(Parser));
			s_SQL = ((!b_Append_LF || s_SQL.EndsWith("\n\n")) ? (s_SQL + "  ") : (s_SQL + "\n\n"));
			ParseText(s_SQL);
			ParseParenthesis();
			ParseItem i_Item = mi_FirstItem;
			ParseCommands(ref i_Item);
			if (mb_ModifyLineBR)
			{
				ParseLineBreaks();
			}
			FillRtfBuilder();
			s32_CursorPos = ms32_CursorPos;
			return !mb_Abort;
		}

		protected static eType getType(ParseItem i_Item)
		{
			return i_Item?.Type ?? eType.Invalid;
		}

		protected static eCmd getCmd(ParseItem i_Item)
		{
			return i_Item?.Cmd ?? eCmd.Undefined;
		}

		protected static string getDebug(ParseItem i_Item)
		{
			if (i_Item == null)
			{
				return "[NULL]";
			}
			return "'" + i_Item.Text.Replace("\n", "\\n") + "'";
		}

		protected static int MeasureLength(ParseItem i_ItemStart, ParseItem i_ItemEnd, StringBuilder s_Sql)
		{
			int num = 0;
			do
			{
				if (s_Sql != null)
				{
					s_Sql.Append(i_ItemStart.Text);
					s_Sql.Append(" ");
				}
				num += i_ItemStart.Text.Length + 1;
				i_ItemStart = i_ItemStart.Next;
			}
			while (!i_ItemStart.Equals(i_ItemEnd));
			return num;
		}

		private void ParseText(string s_SQL)
		{
			s_SQL = s_SQL.Replace("\r", "").Replace("\t", " ");
			mi_FirstItem = null;
			ParseItem i_LastItem = null;
			int length = s_SQL.Length;
			int s32_Start = -1;
			for (int i = 0; i < length; i++)
			{
				Application.DoEvents();
				if (mb_Abort)
				{
					break;
				}
				char c = s_SQL[i];
				if ("!=<> +-*/,()'\n[".IndexOf(c) >= 0)
				{
					if (c == '*' && i > 0 && s_SQL[i - 1] == '.')
					{
						continue;
					}
					if (s32_Start >= 0)
					{
						AddItem(ref s_SQL, ref s32_Start, i, ref i_LastItem);
						if (c != ' ')
						{
							i--;
						}
					}
					else
					{
						if (c == ' ')
						{
							continue;
						}
						s32_Start = i;
						if ("!=<>*".IndexOf(c) >= 0)
						{
							for (; "!=<>*".IndexOf(s_SQL[i + 1]) >= 0; i++)
							{
							}
						}
						else if (c == '\'')
						{
							while (i + 1 < length)
							{
								i++;
								if (s_SQL[i] == '\'')
								{
									if (s_SQL[i + 1] != '\'')
									{
										break;
									}
									i++;
								}
							}
						}
						else if (c == '[')
						{
							while (true)
							{
								if (i + 1 < length && s_SQL[i + 1] != ']' && s_SQL[i + 1] != '\n')
								{
									i++;
									continue;
								}
								i++;
								int j;
								for (j = i; j + 2 < length && (s_SQL[j + 1] == '.' || s_SQL[j + 1] == ' '); j++)
								{
								}
								j++;
								if (j >= length || s_SQL[j] != '[')
								{
									break;
								}
								i = j + 2;
							}
						}
						else if (c == '-' && s_SQL[i + 1] == '-')
						{
							for (; i + 1 < length && s_SQL[i + 1] != '\n'; i++)
							{
							}
						}
						else if (c == '/' && s_SQL[i + 1] == '*')
						{
							for (; i + 2 < length && (s_SQL[i + 1] != '*' || s_SQL[i + 2] != '/'); i++)
							{
							}
							i += 2;
						}
						else if (c == '*' && s_SQL[i + 1] == '/')
						{
							i++;
						}
						AddItem(ref s_SQL, ref s32_Start, i + 1, ref i_LastItem);
					}
				}
				else if (s32_Start < 0)
				{
					s32_Start = i;
				}
			}
		}

		private void AddItem(ref string s_SQL, ref int s32_Start, int s32_End, ref ParseItem i_LastItem)
		{
			int num = Math.Min(s32_End, s_SQL.Length);
			string s_Text = s_SQL.Substring(s32_Start, num - s32_Start);
			ParseItem parseItem = new ParseItem(this, s_Text);
			if (parseItem.Type != eType.Invalid)
			{
				if (mi_FirstItem == null)
				{
					mi_FirstItem = parseItem;
				}
				else
				{
					i_LastItem.InsertAfter(parseItem);
				}
				i_LastItem = parseItem;
				if (!mb_CursorStored && ms32_CursorPos <= s32_End)
				{
					mb_CursorStored = true;
					parseItem.CursorPos = Math.Max(0, ms32_CursorPos - s32_Start);
					Functions.PrintDebug("AddItem() set CursorPos={0} in Item      {1} <{2}> ...", parseItem.CursorPos, getDebug(parseItem.Prev), getDebug(parseItem), typeof(Parser));
				}
			}
			s32_Start = -1;
		}

		private void ParseParenthesis()
		{
			StackEx stackEx = new StackEx();
			for (ParseItem next = mi_FirstItem; next != null; next = next.Next)
			{
				Application.DoEvents();
				if (mb_Abort)
				{
					break;
				}
				switch (next.Type)
				{
				case eType.ParOpen:
					stackEx.Push(next);
					break;
				case eType.ParClose:
				{
					ParseItem parseItem = (ParseItem)stackEx.Pop();
					if (parseItem != null)
					{
						next.Outdent = parseItem.Indent;
					}
					break;
				}
				case eType.LineBreak:
				{
					for (int i = 0; i < stackEx.Count; i++)
					{
						((ParseItem)stackEx[i]).Indent = eIndent.SingleLF;
					}
					break;
				}
				case eType.Comma:
					((ParseItem)stackEx.Peek())?.SetContains(eType.Comma);
					break;
				case eType.CommentL:
					if (getType(next.Prev) == eType.LineBreak)
					{
						next.Prev.Permanent = true;
					}
					ParseItem.SetLinebreakAfter(next, 1);
					break;
				case eType.CommentP:
					if (getType(next.Prev) == eType.LineBreak)
					{
						next.Prev.Permanent = true;
						if (getType(next.Prev.Prev) == eType.LineBreak)
						{
							next.Prev.Prev.Permanent = true;
						}
					}
					break;
				case eType.String:
					if (getType(next.Prev) == eType.Unknown && next.Prev.Text.ToUpper() == "N")
					{
						next.Prev.Remove(b_IgnorePermanent: true);
						next.Text = "N" + next.Text;
					}
					break;
				}
				switch (next.Cmd)
				{
				case eCmd.Case:
				case eCmd.Select:
				{
					for (int k = 0; k < stackEx.Count; k++)
					{
						((ParseItem)stackEx[k]).Indent = eIndent.DoubleLF;
					}
					break;
				}
				case eCmd.AndOr:
				{
					for (int j = 0; j < stackEx.Count - 1; j++)
					{
						((ParseItem)stackEx[j]).Indent = eIndent.DoubleLF;
					}
					if (stackEx.Count > 0)
					{
						((ParseItem)stackEx[stackEx.Count - 1]).Indent = eIndent.SingleLF;
					}
					break;
				}
				case eCmd.Left:
				case eCmd.Right:
					if (getType(next.Next) == eType.ParOpen)
					{
						next.Type = eType.Function;
					}
					break;
				case eCmd.Alter:
					if (getCmd(next.Next) == eCmd.Column)
					{
						next.Type = eType.Keyword;
					}
					break;
				}
			}
		}

		private void ParseCommands(ref ParseItem i_Item)
		{
			while (i_Item != null)
			{
				Application.DoEvents();
				if (mb_Abort)
				{
					break;
				}
				switch (i_Item.Type)
				{
				case eType.ParClose:
					return;
				case eType.ParOpen:
					i_Item = i_Item.Next;
					ParseCommands(ref i_Item);
					break;
				case eType.Comand:
				case eType.Keyword:
					switch (i_Item.Cmd)
					{
					case eCmd.Select:
						Parse_SELECT(ref i_Item);
						break;
					case eCmd.Create:
						Parse_CREATE(ref i_Item);
						break;
					case eCmd.Case:
						Parse_CASE(ref i_Item);
						break;
					case eCmd.Begin:
					{
						ParseItem i_Item2 = i_Item.FindAfterLF();
						if (getCmd(i_Item2) != eCmd.Distributed && getCmd(i_Item2) != eCmd.Transaction)
						{
							i_Item.Indent = eIndent.DoubleLF;
						}
						break;
					}
					case eCmd.End:
						i_Item.Outdent = eIndent.DoubleLF;
						break;
					case eCmd.Else:
						ParseItem.SetLinebreakBefore(i_Item, 1);
						break;
					case eCmd.Exec:
						ParseItem.SetLinebreakBefore(i_Item, 2);
						break;
					}
					break;
				}
				if (i_Item != null)
				{
					i_Item = i_Item.Next;
					continue;
				}
				break;
			}
		}

		private void Parse_SELECT(ref ParseItem i_Item)
		{
			ParseItem parseItem = i_Item;
			ParseItem i_Item2 = null;
			ParseItem parseItem2 = null;
			ParseItem parseItem3 = null;
			ParseItem parseItem4 = null;
			ParseItem i_Item3 = null;
			ParseItem i_Item4 = null;
			ParseItem i_Item5 = null;
			ParseItem i_Item6 = null;
			ParseItem i_Item7 = null;
			ParseItem i_Item8 = null;
			ParseItem i_Item9 = null;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			ArrayList arrayList3 = new ArrayList();
			ArrayList arrayList4 = new ArrayList();
			ArrayList arrayList5 = new ArrayList();
			ArrayList arrayList6 = new ArrayList();
			ParseItem i_ItemStart = i_Item;
			bool flag = false;
			while (i_Item != null && i_Item.Next != null && !flag)
			{
				Application.DoEvents();
				if (mb_Abort)
				{
					return;
				}
				i_Item = i_Item.Next;
				bool flag2 = false;
				switch (i_Item.Type)
				{
				case eType.ParOpen:
					i_Item = i_Item.Next;
					ParseCommands(ref i_Item);
					break;
				case eType.Comand:
				case eType.ParClose:
					i_Item9 = i_Item;
					i_Item = i_Item.Prev;
					flag = true;
					break;
				case eType.Comma:
					flag2 = true;
					if (parseItem2 == null)
					{
						arrayList.Add(i_Item);
					}
					else if (parseItem4 == null)
					{
						arrayList2.Add(i_Item);
					}
					else
					{
						arrayList3.Add(i_Item);
					}
					break;
				case eType.LineBreak:
					arrayList6.Add(i_Item);
					break;
				case eType.Keyword:
					flag2 = true;
					switch (i_Item.Cmd)
					{
					case eCmd.AndOr:
						arrayList5.Add(i_Item);
						break;
					case eCmd.From:
						parseItem2 = i_Item;
						break;
					case eCmd.Group:
						parseItem4 = i_Item;
						break;
					case eCmd.Having:
						i_Item3 = i_Item;
						break;
					case eCmd.Into:
						i_Item2 = i_Item;
						break;
					case eCmd.Join:
						arrayList4.Add(i_Item);
						break;
					case eCmd.Order:
						i_Item4 = i_Item;
						break;
					case eCmd.Union:
						i_Item5 = i_Item;
						break;
					case eCmd.Where:
						parseItem3 = i_Item;
						break;
					case eCmd.Case:
						Parse_CASE(ref i_Item);
						break;
					}
					break;
				}
				if (flag2)
				{
					if (MeasureLength(i_ItemStart, i_Item, null) < 150)
					{
						foreach (ParseItem item in arrayList6)
						{
							item.Remove(b_IgnorePermanent: false);
						}
					}
					i_ItemStart = i_Item;
					arrayList6.Clear();
				}
			}
			ParseItem.SetLinebreakBefore(i_Item9, 2);
			InsertCommas(parseItem, arrayList, parseItem2);
			InsertCommas(parseItem2, arrayList2, parseItem3);
			InsertCommas(parseItem4, arrayList3, null);
			ParseItem.SetLinebreakBefore(parseItem, 1);
			ParseItem.SetLinebreakBefore(i_Item2, 1);
			ParseItem.SetLinebreakBefore(parseItem4, 1);
			ParseItem.SetLinebreakBefore(i_Item3, 1);
			ParseItem.SetLinebreakBefore(i_Item4, 1);
			ParseItem.SetLinebreakBefore(i_Item5, 1);
			ParseItem.SetLinebreakBefore(i_Item6, 1);
			ParseItem.SetLinebreakBefore(i_Item7, 1);
			ParseItem.SetLinebreakBefore(i_Item8, 1);
			for (int i = 0; i < arrayList4.Count; i++)
			{
				ParseItem parseItem5 = (ParseItem)arrayList4[i];
				while (getType(parseItem5.Prev) == eType.Keyword)
				{
					parseItem5 = parseItem5.Prev;
				}
				ParseItem.SetLinebreakBefore(parseItem5, 1);
			}
			if (arrayList5.Count >= 1)
			{
				ParseItem.SetLinebreakBefore(parseItem3, 1);
				foreach (ParseItem item2 in arrayList5)
				{
					ParseItem.SetLinebreakBefore(item2, 1);
					if (getType(item2.Next) == eType.ParOpen)
					{
						ParseItem.SetLinebreakAfter(item2, 1);
					}
				}
			}
		}

		private void Parse_CASE(ref ParseItem i_Item)
		{
			i_Item.Indent = eIndent.SingleLF;
			while (i_Item != null && i_Item.Next != null)
			{
				Application.DoEvents();
				if (mb_Abort)
				{
					break;
				}
				i_Item = i_Item.Next;
				switch (i_Item.Type)
				{
				case eType.LineBreak:
					i_Item.Remove(b_IgnorePermanent: false);
					break;
				case eType.ParOpen:
					i_Item = i_Item.Next;
					ParseCommands(ref i_Item);
					break;
				case eType.ParClose:
					i_Item = i_Item.Prev;
					return;
				case eType.Comand:
				case eType.Keyword:
					switch (i_Item.Cmd)
					{
					case eCmd.When:
						ParseItem.SetLinebreakBefore(i_Item, 1);
						break;
					case eCmd.Else:
						i_Item.Type = eType.Keyword;
						ParseItem.SetLinebreakBefore(i_Item, 1);
						break;
					case eCmd.End:
						i_Item.Type = eType.Keyword;
						i_Item.Outdent = eIndent.DoubleLF;
						return;
					}
					break;
				}
			}
		}

		private void Parse_CREATE(ref ParseItem i_Item)
		{
			i_Item = i_Item.Next;
			switch (getCmd(i_Item))
			{
			case eCmd.Function:
			case eCmd.Procedure:
			case eCmd.Trigger:
			case eCmd.View:
			{
				i_Item = i_Item.Next;
				if (getType(i_Item) != eType.Unknown)
				{
					break;
				}
				ParseItem.SetLinebreakAfter(i_Item, 1);
				int num = 0;
				bool flag = false;
				bool flag2 = true;
				do
				{
					Application.DoEvents();
					if (mb_Abort)
					{
						break;
					}
					i_Item = i_Item.Next;
					switch (getType(i_Item))
					{
					case eType.ParOpen:
						if (num == 0 && i_Item.Contains(eType.Comma))
						{
							i_Item.Indent = eIndent.DoubleLF;
							flag = true;
						}
						num++;
						break;
					case eType.ParClose:
						num--;
						if (flag && num == 0)
						{
							i_Item.Outdent = eIndent.DoubleLF;
							flag = false;
						}
						break;
					case eType.LineBreak:
						if (flag2)
						{
							i_Item.Remove(b_IgnorePermanent: false);
						}
						break;
					case eType.Comma:
						if (getType(i_Item.Next) != eType.CommentL)
						{
							ParseItem.SetLinebreakAfter(i_Item, 1);
						}
						break;
					case eType.Keyword:
						switch (i_Item.Cmd)
						{
						case eCmd.As:
							if (num == 0)
							{
								ParseItem.SetLinebreakBefore(i_Item, 1);
								return;
							}
							break;
						case eCmd.On:
							ParseItem.SetLinebreakBefore(i_Item, 1);
							flag2 = false;
							break;
						case eCmd.Returns:
							ParseItem.SetLinebreakBefore(i_Item, 1);
							break;
						}
						break;
					case eType.Comand:
					{
						eCmd cmd = i_Item.Cmd;
						if (cmd == eCmd.Delete || cmd == eCmd.Insert || cmd == eCmd.Update)
						{
							i_Item.Type = eType.Keyword;
						}
						break;
					}
					}
				}
				while (i_Item != null);
				break;
			}
			}
		}

		private void InsertCommas(ParseItem i_KeyBefore, ArrayList i_Commas, ParseItem i_KeyAfter)
		{
			if (i_Commas.Count == 0)
			{
				return;
			}
			foreach (ParseItem i_Comma in i_Commas)
			{
				ParseItem.SetLinebreakAfter(i_Comma, 1);
			}
			if (i_KeyBefore != null)
			{
				ParseItem next = i_KeyBefore.Next;
				while (getType(next) == eType.Keyword)
				{
					if (getCmd(next) == eCmd.Top && getType(next.Next) == eType.Number)
					{
						next = next.Next;
					}
					next = next.Next;
				}
				ParseItem.SetLinebreakBefore(next, 1);
			}
			ParseItem.SetLinebreakBefore(i_KeyAfter, 1);
		}

		private void ParseLineBreaks()
		{
			ParseItem parseItem = null;
			ParseItem parseItem2 = null;
			ParseItem parseItem3 = null;
			for (parseItem = mi_FirstItem; parseItem != null; parseItem = parseItem.Next)
			{
				Application.DoEvents();
				if (mb_Abort)
				{
					break;
				}
				if (parseItem2 != null && parseItem3 != null)
				{
					switch (parseItem.Type)
					{
					case eType.Operator:
						if (parseItem2.Type == eType.LineBreak)
						{
							parseItem2.Remove(b_IgnorePermanent: true);
						}
						break;
					case eType.ParOpen:
						if (parseItem2.Type == eType.LineBreak && parseItem3.Cmd == eCmd.If)
						{
							parseItem2.Remove(b_IgnorePermanent: true);
						}
						break;
					case eType.ParClose:
					case eType.LineBreak:
						if (parseItem2.Type == eType.LineBreak && parseItem3.Type == eType.LineBreak)
						{
							parseItem2.Remove(b_IgnorePermanent: true);
						}
						break;
					case eType.Comma:
						if (parseItem2.Type == eType.LineBreak && parseItem3.Type == eType.ParClose)
						{
							parseItem2.Remove(b_IgnorePermanent: true);
						}
						break;
					}
					eCmd cmd = parseItem.Cmd;
					if (cmd != eCmd.Between)
					{
						if (cmd == eCmd.Distributed || cmd == eCmd.Transaction)
						{
							while (getType(parseItem.Prev) == eType.LineBreak)
							{
								parseItem.Prev.Remove(b_IgnorePermanent: true);
							}
						}
					}
					else
					{
						ParseItem next = parseItem.Next;
						for (int i = 0; i < 5; i++)
						{
							if (next == null)
							{
								break;
							}
							if (getType(next) == eType.LineBreak && getCmd(next.Next) == eCmd.AndOr)
							{
								next.Remove(b_IgnorePermanent: true);
								break;
							}
							next = next.Next;
						}
					}
					eType type = parseItem3.Type;
					if (type == eType.ParOpen && parseItem2.Type == eType.LineBreak && parseItem.Type == eType.LineBreak)
					{
						parseItem2.Remove(b_IgnorePermanent: true);
					}
				}
				parseItem3 = parseItem2;
				parseItem2 = parseItem;
			}
		}

		private void FillRtfBuilder()
		{
			mi_RtfHtmlBuilder.Clear();
			for (ParseItem next = mi_FirstItem; next != null; next = next.Next)
			{
				Application.DoEvents();
				if (mb_Abort)
				{
					break;
				}
				if (next.CursorPos > -1)
				{
					ms32_CursorPos = mi_RtfHtmlBuilder.RtfPosition + next.CursorPos;
					Functions.PrintDebug("FillRtf() get CursorPos={0} from Item    {1} <{2}> {3}", next.CursorPos, getDebug(next.Prev), getDebug(next), getDebug(next.Next), typeof(Parser));
				}
				next.GetText(mi_RtfHtmlBuilder);
			}
		}

		public string GetRtf(Font i_Font)
		{
			return mi_RtfHtmlBuilder.BuildRtf(i_Font);
		}

		public string AlterCreateCommand(string s_Obj, string s_Name)
		{
			ParseItem parseItem = null;
			ParseItem parseItem2 = null;
			ParseItem parseItem3 = null;
			for (ParseItem next = mi_FirstItem; next != null; next = next.Next)
			{
				if (next.Type != eType.CommentL && next.Type != eType.CommentP && next.Type != eType.LineBreak)
				{
					if (parseItem == null)
					{
						parseItem = next;
					}
					else if (parseItem2 == null)
					{
						parseItem2 = next;
					}
					else if (parseItem3 == null)
					{
						parseItem3 = next;
						break;
					}
				}
			}
			if (getType(parseItem) != eType.Comand || getCmd(parseItem) != eCmd.Create)
			{
				return null;
			}
			if (getType(parseItem2) != eType.Keyword || parseItem2.Text != s_Obj.ToUpper())
			{
				return null;
			}
			if (getType(parseItem3) != eType.Unknown)
			{
				return null;
			}
			string[] array = parseItem3.Text.Split('.');
			if (string.Compare(array[array.Length - 1].Replace("[", "").Replace("]", "").Trim(), s_Name, ignoreCase: true) != 0)
			{
				return null;
			}
			parseItem.Text = "ALTER";
			FillRtfBuilder();
			string sQL = SQL;
			parseItem.Text = "CREATE";
			FillRtfBuilder();
			return sQL;
		}
	}
}
