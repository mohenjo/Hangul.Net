# Hangul.Net | 한글 처리 클래스 라이브러리

C# Hangul.Net 클래스 라이브러리는 한글 문자/문자열에 대해 다양한 속성/메서드를 제공합니다.



## Features

+ 한글 음소(자음/모음, 초성/중성/종성) 및 음절 판단
+ 한글 음소 분리 및 합성
+ 한글 획수 계산
+ 한글 초성 검색
+ 문자열의 길이 / 바이트 길이 반환
+ `char` 및 `string` 자료형에 대해 각각 `HangulChar` 및 `HangulString` 클래스 제공



## Get Started

+ 클래스 라이브러리(DLL) [다운로드](https://github.com/mohenjo/Hangul.Net/raw/master/Download/HPark.Hangul.dll) 
+ 참조: `Using HPark.Hangul`



## Desciption of Classes

### HangulChar

+ 생성자
    + `public HangulChar(char character)`
+ 속성
    + `public char CurrentCharacter { get; private set; }`
      + 현재 인스턴스의 문자입니다.
    + `public int CurrentUnicode { get; }`
      + 현재 인스턴스 문자의 유니코드입니다.
+ 메소드
    + `public bool IsOnsetMatch(char searchChar)`
      + 검색문자를 현재 인스턴스의 문자에 대해 (초성) 비교 후 일치 여부를 반환합니다.
         검색문자에 초성이 주어질 경우 초성 일치, 그렇지 않은 경우 문자 완전 일치 여부를 반환합니다.
    + `public bool IsOnset()`
       + 초성으로 사용될 수 있는지 판단합니다.
    + `public bool IsNucleus()`
       + 중성으로 사용될 수 있는지 판단합니다.
    + `public bool IsCoda()`
       + 종성으로 사용될 수 있는지 판단합니다.
    + `public bool IsConsonant()`
       + 자음인지 판단합니다.
    + `public bool IsVowel()`
       + 모음인지 판단합니다.
    + `public bool IsPhoneme()`
       + 음소(낱소리)인지 판단합니다.
    + `public bool IsSyllable()`
       + 음소(낱소리)가 아닌 완전한 한글 음절인지 판단합니다.
    + `public bool IsKoreanCharacter()`
       + 한글 문자인지 판단합니다.
    + `public bool IsHangul()`
       + 한글 문자인지 판단합니다. (=` IsKoreanCharacter()`)
    + `public bool TrySplitSyllable(out char[] phonemes)`
       + 한글 음절을 초성, 중성, 종성 순으로 분리합니다. 반환 결과는 분리의 성공(가능)여부를 나타냅니다.
    + `public char[] SplitSyllable()`
       + 한글 음절을 초성, 중성, 종성 순으로 분리합니다. 분리가 불가능한 경우 예외를 발생시킵니다.
    + `public int CountStrokes()`
       + 한글 획수를 반환합니다. 한글이 아닌 문자의 경우 0을 반환합니다.
    + `public static bool TryJoinToSyllable(char[] phonemes, out char syllable)`
      + 인자의 음소 배열을 한글 음절로 합성합니다. 반환값은 합성의 성공(가능) 여부를 나타냅니다.
    + `public static char JoinToSyllable(char[] phonemes)`
      + 인자의 음소 배열을 한글 음절로 합성합니다. 합성이 불가능할 경우 예외를 발생시킵니다.
    + `public static bool IsOnsetMatch(char searchChar, char targetChar)`
      + 검색문자를 대상문자에 대해 (초성) 비교 후 일치 여부를 반환합니다. 검색문자에 초성이 주어질 경우 초성 일치, 그렇지 않은 경우 문자 완전 일치 여부를 반환합니다.


### HangulString

+ 생성자
    + `public HangulString(string aString)`
+ 속성
    + `public string CurrentString { get; private set; }`
        + 현재 인스턴스의 문자열입니다.
+ 메소드
    + `public HangulChar[] ToHangulCharArray()`
        + 현재 인스턴스의 문자열을 `HangulChar` 클래스 인스턴스의 배열로 변환하여 반환합니다.
    + `public string[] SeparateString()`
        + 현재 인스턴스의 문자열을 한글 문자열 부분과 나머지 문자열 부분으로 구분하여 반환합니다.
    + `public bool IsAllHangul()`
        + 현재 인스턴스의 문자열이 모두 한글로만 이루어져 있는지의 여부를 반환합니다.
    + `public string SplitToPhonemes()`
        + 현재 인스턴스의 문자열에 대해 한글 음절을 초성, 중성, 종성으로 분리하여 반환합니다.
    + `public string JoinPhonemes()`
        + 인스턴스 문자열 내의 초성, 중성, 종성 음소를 합성하여 반환합니다.
    + `public int GetStringLength(bool ignoreWhiteSpcaes = false)`
        + 인스턴스 문자열의 길이(글자수)를 반환합니다.
    + `public int GetStringByteLength(bool ignoreWhiteSpcaes = false)`
        + 현재 인코딩에 대해 인스턴스 문자열의 길이(바이트)를 반환합니다.
    + `public static HangulChar[] ToHangulCharArray(string aString)`
        + 문자열을 `HangulChar` 클래스 인스턴스의 배열로 변환하여 반환합니다.
    + `public static string[] SeparateString(string aString)`
        + 문자열을 한글 문자열 부분과 나머지 문자열 부분으로 구분하여 반환합니다.
    + `public static bool IsAllHangul(string aString)`
        + 문자열이 모두 한글로만 이루어져 있는지의 여부를 반환합니다.
    + `public static string SplitToPhonemes(string aString)`
        + 문자열에 대해 한글 음절을 초성, 중성, 종성으로 분리하여 반환합니다.
    + `public static string JoinPhonemes(string aString)`
        + 문자열 내의 초성, 중성, 종성 음소를 합성하여 반환합니다.
    + `public static bool GetOnsetMatches(string searchString, string targetString, out int[] indices)`
        + 문자열에 대해 초성 검색을 실시합니다. 반환값은 초성 검색에 대한 결과의 존재여부입니다.  검색 문자열에 초성이 주어질 경우 초성 일치, 그렇지 않은 경우 문자 완전 일치 여부를 반환합니다.



## Usage

+ 생성자, 속성, 메서드 등에 대한 자세한 내용은 코드 내 XML 문서 주석을 참조하시기 바랍니다.
+ 사용 예제는  [`Hpark.Net.Usage`](https://github.com/mohenjo/Hangul.Net/tree/master/Hangul.Net/Hpark.Hangul.Usage) 프로젝트를 참조하시기 바랍니다.



## Project Info

### Version

+ Version 1.1909

### Dev Tools

+ [Visual Studio Community 2019](https://visualstudio.microsoft.com/ko/vs/)

### Environments

+ Test Environment

    + Microsoft Windows 10 (x64)
    + Microsoft .NET framework 4.7.2

+ Dependencies / 3rd-party package(s)

    + None




## License

+ [MIT License](https://github.com/mohenjo/Hangul.Net/blob/master/LICENSE)
