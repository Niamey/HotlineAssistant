using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Vostok.Hotline.Core.Common.Helpers
{
	public static class TranslitHelper
	{
		private static char ToUpperCase(char ch)
		{
			return char.ToUpper(ch);
		}

		private static char ChangeRegister(char ch, bool toLowerCase)
		{
			return toLowerCase ? char.ToLower(ch) : ch;
		}

		public static bool IsCyrillic(string value)
		{
			return Regex.IsMatch(value, @"\p{IsCyrillic}");
		}

		public static bool IsBasicLatin(string value)
		{
			return Regex.IsMatch(value, @"\p{IsBasicLatin}");
		}

		public static string LatinToCyrillic(string s, bool withTranslit)
		{
			StringBuilder sb = new StringBuilder(s.Length);
			int i = 0;
			while (i < s.Length)
			{// Идем по строке слева направо. В принципе, подходит для обработки потока
				char ch = s[i];
				bool lc = char.IsLower(ch); // для сохранения регистра
				ch = ToUpperCase(ch);

				if (withTranslit)
				{
					// транслитерация
					if (ch == 'J')
					{ // Префиксная нотация вначале
						i++; // преходим ко второму символу сочетания
						ch = ToUpperCase(s[i]);
						switch (ch)
						{
							case 'O': sb.Append(ChangeRegister('Ё', lc)); break;
							case 'H':
								if (i + 1 < s.Length && ToUpperCase(s[i + 1]) == 'H')
								{ // проверка на постфикс (вариант JHH)
									sb.Append(ChangeRegister('Ъ', lc));
									i++; // пропускаем постфикс
								}
								else
								{
									sb.Append(ChangeRegister('Ь', lc));
								}
								break;
							case 'U': sb.Append(ChangeRegister('Ю', lc)); break;
							case 'A': sb.Append(ChangeRegister('Я', lc)); break;
							default: throw new ArgumentException(
								$"Illegal transliterated symbol '{ch}' at position {i}");
						}
					}
					else if (i + 1 < s.Length && ToUpperCase(s[i + 1]) == 'H')
					{// Постфиксная нотация, требует информации о двух следующих символах. Для потока придется сделать обертку с очередью из трех символов.
						switch (ch)
						{
							case 'Z': sb.Append(ChangeRegister('Ж', lc)); break;
							case 'K': sb.Append(ChangeRegister('Х', lc)); break;
							case 'C': sb.Append(ChangeRegister('Ч', lc)); break;
							case 'S':
								if (i + 2 < s.Length && ToUpperCase(s[i + 2]) == 'H')
								{ // проверка на двойной постфикс
									sb.Append(ChangeRegister('Щ', lc));
									i++; // пропускаем первый постфикс
								}
								else
								{
									sb.Append(ChangeRegister('Ш', lc));
								}
								break;
							case 'E': sb.Append(ChangeRegister('Э', lc)); break;
							case 'I': sb.Append(ChangeRegister('Ы', lc)); break;
							default: throw new ArgumentException(
								$"Illegal transliterated symbol '{ch}' at position {i}");
						}
						i++; // пропускаем постфикс
					}
					else
					{// одиночные символы
						switch (ch)
						{
							case 'A': sb.Append(ChangeRegister('А', lc)); break;
							case 'B': sb.Append(ChangeRegister('Б', lc)); break;
							case 'V': sb.Append(ChangeRegister('В', lc)); break;
							case 'G': sb.Append(ChangeRegister('Г', lc)); break;
							case 'D': sb.Append(ChangeRegister('Д', lc)); break;
							case 'E': sb.Append(ChangeRegister('Е', lc)); break;
							case 'Z': sb.Append(ChangeRegister('З', lc)); break;
							case 'I': sb.Append(ChangeRegister('И', lc)); break;
							case 'Y': sb.Append(ChangeRegister('Й', lc)); break;
							case 'K': sb.Append(ChangeRegister('К', lc)); break;
							case 'L': sb.Append(ChangeRegister('Л', lc)); break;
							case 'M': sb.Append(ChangeRegister('М', lc)); break;
							case 'N': sb.Append(ChangeRegister('Н', lc)); break;
							case 'O': sb.Append(ChangeRegister('О', lc)); break;
							case 'P': sb.Append(ChangeRegister('П', lc)); break;
							case 'R': sb.Append(ChangeRegister('Р', lc)); break;
							case 'S': sb.Append(ChangeRegister('С', lc)); break;
							case 'T': sb.Append(ChangeRegister('Т', lc)); break;
							case 'U': sb.Append(ChangeRegister('У', lc)); break;
							case 'F': sb.Append(ChangeRegister('Ф', lc)); break;
							case 'C': sb.Append(ChangeRegister('Ц', lc)); break;
							default: sb.Append(ChangeRegister(ch, lc)); break;
						}
					}
				}
				else 
				{
					//перемап от визиальных ошибок
					switch (ch)
					{
						case 'A': sb.Append(ChangeRegister('А', lc)); break;
						case 'B': sb.Append(ChangeRegister('В', lc)); break;
						case 'E': sb.Append(ChangeRegister('Е', lc)); break;
						case 'K': sb.Append(ChangeRegister('К', lc)); break;
						case 'M': sb.Append(ChangeRegister('М', lc)); break;
						case 'H': sb.Append(ChangeRegister('Н', lc)); break;
						case 'O': sb.Append(ChangeRegister('О', lc)); break;
						case 'C': sb.Append(ChangeRegister('С', lc)); break;
						case 'T': sb.Append(ChangeRegister('Т', lc)); break;
						case 'X': sb.Append(ChangeRegister('Х', lc)); break;
						case 'Y': sb.Append(ChangeRegister('У', lc)); break;
						case 'P': sb.Append(ChangeRegister('Р', lc)); break;
						case 'I': sb.Append(ChangeRegister('І', lc)); break;
						default: sb.Append(ChangeRegister(ch, lc)); break;
					}
				}

				i++; // переходим к следующему символу
			}
			return sb.ToString();
		}

		public static string CyrillicToLatin(char ch, bool withTranslit)
		{
			if (withTranslit)
			{
				switch (ch)
				{
					case 'А': return "A";
					case 'Б': return "B";
					case 'В': return "V";
					case 'Г': return "G";
					case 'Д': return "D";
					case 'Е': return "E";
					case 'Ё': return "JO";
					case 'Ж': return "ZH";
					case 'З': return "Z";
					case 'И': return "I";
					case 'Й': return "Y";
					case 'К': return "K";
					case 'Л': return "L";
					case 'М': return "M";
					case 'Н': return "N";
					case 'О': return "O";
					case 'П': return "P";
					case 'Р': return "R";
					case 'С': return "S";
					case 'Т': return "T";
					case 'У': return "U";
					case 'Ф': return "F";
					case 'Х': return "KH";
					case 'Ц': return "C";
					case 'Ч': return "CH";
					case 'Ш': return "SH";
					case 'Щ': return "SHH";
					case 'Ъ': return "JHH";
					case 'Ы': return "IH";
					case 'Ь': return "JH";
					case 'Э': return "EH";
					case 'Ю': return "JU";
					case 'Я': return "JA";
					default: return ch.ToString();
				}
			}

			switch (ch)
			{
				case 'А': return "A";
				case 'В': return "B";
				case 'Е': return "E";
				case 'К': return "K";
				case 'М': return "M";
				case 'Н': return "H";
				case 'О': return "O";
				case 'С': return "C";
				case 'Т': return "T";
				case 'Х': return "X";
				case 'У': return "Y";
				case 'Р': return "P";
				case 'І': return "I";

				default: return ch.ToString();
			}
		}

		public static string CyrillicToLatin(string s, bool withTranslit)
		{
			StringBuilder sb = new StringBuilder(s.Length * 2);
			foreach (char ch in s)
			{
				char upCh = ToUpperCase(ch);
				string lat = CyrillicToLatin(upCh, withTranslit);
				if (ch != upCh)
				{
					lat = lat.ToLower();
				}
				sb.Append(lat);
			}
			return sb.ToString();
		}
	}
}
