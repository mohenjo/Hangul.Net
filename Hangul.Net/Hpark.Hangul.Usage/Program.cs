using HPark.Hangul;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hpark.Hangul.Usage
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("***** HangulChar(한글 문자) 클래스 *****");

			#region 인스턴스 생성 및 엑세스
			Console.WriteLine("\n[인스턴스 생성 및 엑세스]");
			HangulChar hc;
			hc = new HangulChar('ㄺ'); // -> 유니코드: 12602(0x313A)
			Console.WriteLine($"문자: {hc.CurrentCharacter} -> 유니코드: {hc.CurrentUnicode}(0x{hc.CurrentUnicode:X})");
			hc = new HangulChar(54644); // (0xD574) -> 해당 문자: '해'
			Console.WriteLine($"유니코드: {hc.CurrentUnicode}(0x{hc.CurrentUnicode:X}) -> 문자: {hc.CurrentCharacter}");
			#endregion

			#region 문자 판단
			Console.WriteLine("\n[문자 판단]");
			char[] cases = new[] { 'ㄱ', 'ㄸ', 'ㅄ', 'a', 'ㅢ', '한' };
			foreach (char c in cases)
			{
				hc = new HangulChar(c);
				Console.WriteLine($"{hc.CurrentCharacter}:");
				Console.WriteLine($" 초성: {hc.IsOnset()}, 중성: {hc.IsNucleus()}, 종성: {hc.IsCoda()}");
				Console.WriteLine($" 자음: {hc.IsConsonant()}, 모음: {hc.IsVowel()}");
				Console.WriteLine($" 낱소리(음소): {hc.IsPhoneme()}, 음절: {hc.IsSyllable()}");
				Console.WriteLine($" 한글문자: {hc.IsHangul()}"); // = hc.IsKoreanCharacter()
			}
			#endregion

			#region 초중종성 분리
			Console.WriteLine("\n[초성/중성/종성의 분리]");
			cases = new[] { 'ㅎ', 'ㅐ', 'P', '대', '한' };
			foreach (char c in cases)
			{
				hc = new HangulChar(c);
				// hc.SplitSyllable() 메소드의 반환값은 분리된 초성/중성/종성 배열. 분리 불가능 시 예외 발생
				// hc.TrySplitSyllable() 메소드의 반환값은 분리 성공 여부. out 인자는 분리된 초성/중성/종성 배열.
				bool isSuccess = hc.TrySplitSyllable(out char[] phonemes);
				Console.Write($"{hc.CurrentCharacter}: {isSuccess} -> ");
				if (isSuccess)
				{
					// 분리 시 종성이 존재하지 않을 경우, 해당 인덱스에 null char (0x00)가 지정됨
					Console.WriteLine($" ({string.Join(", ", phonemes)})");
				}
				else
				{
					Console.WriteLine();
				}
			}
			#endregion

			#region 한글 획수
			Console.WriteLine("\n[한글 획수]");
			cases = new[] { 'Q', 'ㄱ', '값', '또' };
			foreach (char c in cases)
			{
				hc = new HangulChar(c);
				Console.WriteLine($"{hc.CurrentCharacter}: {hc.CountStrokes()}획");
			}
			#endregion

			#region 초중종성 합성
			Console.WriteLine("\n[초성/중성/종성의 (하나의 음절로) 합성]");
			List<char[]> joinCases = new List<char[]>();
			joinCases.Add(new[] { 'ㄷ', 'ㅐ', (char)0x00 }); // 종성이 없을 경우 입력 방법1
			joinCases.Add(new[] { 'ㄷ', 'ㅐ' }); // // 종성이 없을 경우 입력 방법2
			joinCases.Add(new[] { 'ㅎ', 'ㅏ', 'ㄴ' });
			joinCases.Add(new[] { 'ㅎ', 'ㅏ', 'ㄸ' });
			joinCases.Add(new[] { 'ㅄ', 'ㅣ' });
			joinCases.Add(new[] { 'Z', 'ㅏ' });
			joinCases.Add(new[] { '박' });
			joinCases.Add(new[] { 'ㅆ', 'ㅣ', 'ㅅ', 'ㅑ', 'ㅍ' });
			joinCases.Add(new[] { 'ㅅ', 'ㅑ', 'ㅍ' });
			foreach (char[] joinCase in joinCases)
			{
				// HangulChar.JoinToSyllable() 메소드의 반환값은 합성된 한글 음절. 합성 불가능 시 예외 발생
				// HangulChar.TryJoinToSyllable() 메소드의 반환값은 합성 가능 여부. out 인자는 합성된 한글 음절
				bool isJoinable = HangulChar.TryJoinToSyllable(joinCase, out char syllable);
				syllable = isJoinable ? syllable : (char)0x00;
				Console.WriteLine($"({string.Join(", ", joinCase)}): {isJoinable} -> {syllable}");
			}
			#endregion

			#region 초성비교
			Console.WriteLine("\n[(초성)비교]");
			List<char[]> onsetMatchCases = new List<char[]>();
			onsetMatchCases.Add(new[] { '대', '대' });
			onsetMatchCases.Add(new[] { 'ㄷ', '대' });
			onsetMatchCases.Add(new[] { 'ㅎ', 'ㅎ' });
			onsetMatchCases.Add(new[] { '한', 'ㅎ' });
			onsetMatchCases.Add(new[] { 'A', 'A' });
			Console.WriteLine("정적 메서드:");
			foreach (char[] omc in onsetMatchCases)
			{
				Console.Write($" {omc[0]} -> {omc[1]}: ");
				Console.WriteLine($"{HangulChar.IsOnsetMatch(omc[0], omc[1])}");
			}
			Console.WriteLine("인스턴스 메서드:");
			foreach (char[] omc in onsetMatchCases)
			{
				HangulChar foo = new HangulChar(omc[1]);
				Console.Write($" {omc[0]} -> {foo.CurrentCharacter}: ");
				Console.WriteLine($"{foo.IsOnsetMatch(omc[0])}");
			}
			#endregion

			Console.WriteLine("\n\n");
			Console.WriteLine("***** HangulString(한글 문자열) 클래스 *****");

			#region 인스턴스 생성 및 엑세스
			Console.WriteLine("\n[인스턴스 생성 및 엑세스]");
			HangulString hs = new HangulString("대한민국");
			Console.WriteLine($"instance from string: {hs.CurrentString}");
			#endregion

			#region 문자열 길이
			Console.WriteLine("\n[문자열의 길이]");
			string[] strcases = new string[] { "이것은 문자열1입니다", "This is 2nd문자열입니다" };
			foreach (string strcase in strcases)
			{
				hs = new HangulString(strcase);
				Console.WriteLine($"\"{hs.CurrentString}\":");
				Console.WriteLine($" 문자열 길이(공백포함): {hs.GetStringLength()}");
				Console.WriteLine($" 문자열 길이(공백제거): {hs.GetStringLength(true)}");
				Console.WriteLine($" 바이트 길이(공백포함): {hs.GetStringByteLength()}");
				Console.WriteLine($" 바이트 길이(공백제거): {hs.GetStringByteLength(true)}");
			}
			#endregion

			#region HangulChar 클래스로 변환
			Console.WriteLine("\n[문자열을 HangulChar 클래스 인스턴스의 배열로 변환]");
			Console.WriteLine("정적 메서드:");
			string aString = "대한민국ㄱㄴㄷㄹAB78!#";
			string resultStr = string.Join(", ", HangulString.ToHangulCharArray(aString).Select(hce => hce.CurrentCharacter));
			Console.WriteLine($" {aString} -> HangulChar[]{{{resultStr}}}");
			Console.WriteLine("인스턴스 메서드:");
			hs = new HangulString(aString);
			resultStr = string.Join(", ", hs.ToHangulCharArray().Select(hce => hce.CurrentCharacter));
			Console.WriteLine($" {aString} -> HangulChar[]{{{resultStr}}}");
			#endregion

			#region 문자열을 한글과 나머지 부분으로 구분
			Console.WriteLine("\n[문자열을 한글 부분과 나머지 부분으로 구분 반환]");
			Console.WriteLine("정적 메서드:");
			aString = "대한민국123ABCㄱㄴㄷㄹ!#";
			resultStr = string.Join(", ", HangulString.SeparateString(aString));
			Console.WriteLine($" {aString} -> string[]{{{resultStr}}}");
			Console.WriteLine("인스턴스 메서드:");
			hs = new HangulString(aString);
			resultStr = string.Join(", ", hs.SeparateString());
			Console.WriteLine($" {aString} -> string[]{{{resultStr}}}");
			#endregion

			#region 문자열 한글 구성 여부
			Console.WriteLine("\n[문자열이 한글 문자로만 이루어져 있는지의 여부를 반환]");
			Console.WriteLine("정적 메서드:");
			aString = "대한민국123ABCㄱㄴㄷㄹ!#";
			Console.WriteLine($" {aString} -> {HangulString.IsAllHangul(aString)}");
			Console.WriteLine("인스턴스 메서드:");
			hs = new HangulString(aString);
			Console.WriteLine($" {aString} -> {hs.IsAllHangul()}");
			Console.WriteLine("정적 메서드:");
			aString = "대한민국ㄱㄴㄷㄹ";
			Console.WriteLine($" {aString} -> {HangulString.IsAllHangul(aString)}");
			Console.WriteLine("인스턴스 메서드:");
			hs = new HangulString(aString);
			Console.WriteLine($" {aString} -> {hs.IsAllHangul()}");
			#endregion

			#region 문자열 초중종성 분리
			Console.WriteLine("\n[문자열의 한글 음절 부분을 초성, 중성, 종성으로 분리 또는 합성]");
			Console.WriteLine("정적 메서드:");
			aString = "대한민국12ABㄱㄴ!# 한글 만!세! ";
			string splittedString = HangulString.SplitToPhonemes(aString);
			Console.WriteLine($" {aString} -> {splittedString}");
			Console.WriteLine($" {splittedString} -> {HangulString.JoinPhonemes(splittedString)}");
			Console.WriteLine("인스턴스 메서드:");
			hs = new HangulString(aString);
			Console.WriteLine($" {aString} -> {hs.SplitToPhonemes()}");
			hs = new HangulString(splittedString);
			Console.WriteLine($" {splittedString} -> {hs.JoinPhonemes()}");
			#endregion

			#region 초성검색
			Console.WriteLine("\n[문자열에 대한 초성 검색]");
			List<string[]> stringOnsetMatchCases = new List<string[]>();
			stringOnsetMatchCases.Add(new[] { "대한민국", "ㄷㅎㅁ국" });
			stringOnsetMatchCases.Add(new[] { "ㄷ한ㅁㄱ", "우리대한민국" });
			stringOnsetMatchCases.Add(new[] { "ㄷ한ㅁㄱ", "대한" });
			stringOnsetMatchCases.Add(new[] { "ㄴㄹ", "우리ㄴㄹ우리나라우리누리" });
			stringOnsetMatchCases.Add(new[] { "ㄴ라", "우리ㄴㄹ우리나라우리누리" });
			foreach (string[] mcases in stringOnsetMatchCases)
			{
				Console.Write($" search: {mcases[0]}, target: {mcases[1]} -> 일치여부: ");
				bool result = HangulString.GetOnsetMatches(mcases[0], mcases[1], out int[] indices);
				Console.Write($"{result}");
				if (result)
				{
					Console.WriteLine($" 위치: {string.Join(", ", indices)}");
				}
				else
				{
					Console.WriteLine();
				}
			}
			#endregion

			Console.ReadKey();
		}
	}
}
