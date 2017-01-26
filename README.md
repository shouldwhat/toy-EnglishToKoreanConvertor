# dotnet-EnglishToKoreanUtil

-. 영어 원서 독해 시 단어 서칭을 조금 더 편하게 도와주는 프로그램입니다.

-. 사용법

	(1). 프로그램 실행.
	
	(2). 돋보기 모양의 트레이 아이콘으로 정상적인 실행을 확인.
	
	(3). 찾으려는 영어 단어를 복사(Ctrl + c)
	
	(4). 'F2' 버튼 푸쉬.
	
	(5). 현재 마우스 포인터 아래에 출력된 툴팁 확인.
	
-. 사용 기술

	(1). C# WinForm.
	
	(2). 윈도우 레벨에서의 키보드 이벤트 후킹.
	
	(3). Http Request.
	
	(4). Html Parsing.


-. 클래스 설명

	(1) globalKeyboardHook.cs : 키보드 이벤트를 전역으로 감지하는 역할.
	
	(2). HtmlParsingUtil.cs : Html 문서에서 원하는 Node를 Parsing하는 역할.
	
	(3). HttpClientUtil.cs : HTTP request를 실행하는 역할.
	
	(4). ISearchingService.cs : HTTP request과 Html Parsing 작업에 대한 인터페이스.
	
	(5). SearchingService.cs : ISearchingService 인터페이스에 대한 구현체.