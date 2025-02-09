# AndroidGraphicsApiSelection
Android 기기에서 Graphics API 선택하는 게임 내 옵션을 구현하는 예제입니다.

## 상세 설명
Android 플랫폼 버전 개발에 대한 Unity 공식 문서
- [기본 Unity Activity를 확장하여, command-line 인자를 명시하는 법](https://docs.unity3d.com/2022.3/Documentation/Manual/android-custom-activity-command-line.html)
- [기본 Unity Activity를 확장하는 법](https://docs.unity3d.com/2022.3/Documentation/Manual/android-custom-activity.html)
- [Java와 Kotlin 소스 플러그인](https://docs.unity3d.com/2022.3/Documentation/Manual/AndroidJavaSourcePlugins.html)
- [Unity Standalone Player command-line 인자들](https://docs.unity3d.com/2022.3/Documentation/Manual/PlayerCommandLineArguments.html)
- [Android Manifest 재정의하는 법](https://docs.unity3d.com/kr/current/Manual/overriding-android-manifest.html)
- [PlayerPrefs](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/PlayerPrefs.html)

들을 종합하여 게임 내에서 Graphics API 설정할 수 있는 옵션을 구현한 예제입니다.

이 예제는 Android 게임에서 원하는 Graphics API를 임의로 선택할 수 있어야 한다는 필요성에서 구현된 예제입니다.
Unity 공식 문서 상에 제시된 기본 Unity Activity 확장 및 command-line 인자 명시를 통해 Graphics API 설정을 구현하고 있습니다.

게임 내에서 설정한 값을 게임 시작 시점에 사용하기 위해서, Unity의 PlayerPrefs가 Android빌드에서는 Android SharedPreferences를 이용하여 구현되어 있다는 점을 이용합니다.
PlayerPrefs로 저장하고, SharedPreference로 읽어 들이는 방식으로 구현했습니다.

Unity의 PlayerPrefs로 저장 후 앱 크래시가 발생하면, 다음 실행 시작 시점에서 ShapredPreferences를 통해 읽어들였을 때 최근에 저장한 값을 읽을 수 없고, 이전 값이 읽힙니다(게임이 다시 실행되면 최근 값으로 덮어써집니다.)(저는 Android를 잘 몰라서 그런지 특이하다고 생각되네요.).\
예시(정상): 옵션을 A에서 B로 변경 -> 앱 종료 -> 재시작 시 B로 읽힘 -> 정상.\
예시(크래시): 옵션을 A에서 B로 변경 -> 앱 크래시 -> 재시작 시 A로 읽힘 -> 게임 시작된 후 B로 다시 저장됨 -> 다음 재시작 시 B로 읽힘.\
값 수정 후 PlayerPrefs.Save()를 호출하면 크래시가 되더라도 안전하게 값이 유지가 됩니다.

Unity Activity 확장 시에는 간단하게 Java Kotlin 소스 플러그인 방식을 이용했습니다. 필요하면 다른 방법으로 교체하면 될 것 같습니다.

다른 command-line인자도 비슷한 방식으로 설정이 가능합니다.
그러나 솔직히 일반적인 게임 개발에서 이러한 command-line 인자 설정 행위가 얼마나 쓸모 있을 지 잘 모르겠네요. 적어도 Graphics API에 관련해서는 곧 사용할 필요가 없어지지 않을까 싶습니다.

### 주요 파일들
- Assets\Plugins\Android\JavaSourcePlugins\PreferenceManager.java
  - 앱 시작 단계에서 SharedPreferences에 쉽게 접근하기 위한 클래스입니다. [[Android] SharedPreferences 사용하기 by 숲속의 작은 이야기](https://re-build.tistory.com/37)에서 가져와 썼습니다(아래의 '사용한 코드' 부분에도 명시하였습니다.).
- Assets\Plugins\Android\JavaSourcePlugins\AndroidGraphicsApiSelectionActivity.java
  - 기본 Unity Activity 를 확장하여, 앱 시작 단계에서 SharedPreferences 에 설정된 값에 따라 command-line 인자를 통해 원하는 Graphics API를 설정합니다.
- Assets\Plugins\Android\AndroidManifest.xml
  - 기본 Unity Activity를 확장한 AndroidGraphicsApiSelectionActivity 를 시작점으로 만들기 위해 Android Menifest를 재정의한 것입니다.
- Assets\_Scripts\AndroidGraphicsApiSelection.cs
  - 게임 상에서 PlayerPrefs 에 Graphics API 설정을 기록합니다.
  - 또한, 예외 발생 시 PlayerPrefs의 보존에 대한 문제를 보이기 위해서 Player Prefs 저장, 일반 종료, 강제 크래시 등을 버튼 입력을 통해 수행합니다.
- Assets\Scenes\SampleScene.unity
  - 사용한 씬입니다.

## 사용한 코드
(라이센스가 명시되어 있지 않았으나, 포스팅의 성격이 소스 코드 및 지식 공유에 있다고 판단하고 감사히 가져다 썼습니다. 문제가 될 시 대체하여 구현하겠습니다.)

[Android] SharedPreferences 사용하기 by 숲속의 작은 이야기 -- https://re-build.tistory.com/37 -- 라이센스 명시 없음.