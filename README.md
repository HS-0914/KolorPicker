# 🎨 KolorPicker - 색상 추출 및 팔레트 저장 도구

> **KolorPicker**(Kosta + Color Picker)는 C# WinForms로 개발된 유틸리티 앱으로, 색상 추출과 팔레트 관리를 제공합니다.

## 🤔 개발 배경

디자인이나 개발 중 화면의 색상을 빠르게 추출하고 효율적으로 관리할 수 있는 도구가 필요했습니다.
PowerToys처럼 여러 강력한 유틸이 포함된 프로그램도 있지만, PowerToys에서 실제로 사용하는 기능은 Color Picker뿐이었기에, 오직 색상 추출 기능만 담은 경량 도구를 직접 만들고자 C#과 Windows Forms로 개발하게 되었습니다.

## ✍️ 느낀점

KolorPicker는 처음으로 진행한 C# 프로젝트로, C# 언어와 WinForms 환경을 익히는 경험이 되었습니다. 이 프로젝트를 통해 폼 이벤트 처리, 전역 단축키 등록, GDI+를 활용한 색상 추출, JSON 파일 입출력 등 C#의 기능들을 실습하며 학습했습니다. 이처럼 실용적인 기능을 구현해본 경험은 이후 다른 언어와 플랫폼에서도 응용할 수 있을거라 생각했습니다.

## ✨ 주요 기능

- **색상 추출**  
  마우스 커서 위치의 색상을 실시간으로 추출하고, HEX 및 RGB 값을 표시하며 자동으로 클립보드에 복사합니다.

- **팔레트 저장 및 관리**  
  추출한 색상을 HEX, RGB, 라벨 정보와 함께 팔레트로 저장하고, 리스트 형태로 관리하며 JSON 파일로 저장/불러오기 기능을 제공합니다.

- **돋보기 (Zoom-in)**  
  마우스 주변 50x50 픽셀을 확대하여 더 정밀한 색상 추출이 가능하며, 확대 배율(2~10)을 조절할 수 있습니다.

- **전역 단축키**  
  백그라운드에서도 동작하는 `Ctrl + Shift + C` 단축키로 빠르게 색상 추출을 토글할 수 있습니다.

- **그 외**  
  시스템 트레이 아이콘으로 최소화 시에도 접근 가능하며, 토스트 메시지로 사용자 피드백을 제공합니다.

## 🖼️ 주요 화면

- 색상 추출 및 확대

![색상추출_코드복사](https://github.com/user-attachments/assets/866a3083-87de-4275-8933-cf4afba7739a)

<br>

- 팔레트 저장 및 관리

![팔레트 저장 및 관리](https://github.com/user-attachments/assets/01bc7a3c-8338-4de9-a53a-06c0b01c25ae)

<br>

- 기타 기능(시스템 트레이 실행)

<img src="https://github.com/user-attachments/assets/3204fb64-f15b-43f9-888d-079fe8cc6ef2" width="400" />

---

## 🛠️ 사용 기술

| 항목        | 기술                                                                        |
| ----------- | --------------------------------------------------------------------------- |
| 플랫폼      | Windows Forms (.NET Framework)                                              |
| 언어        | C#                                                                          |
| 단축키 처리 | [Gma.System.MouseKeyHook](https://github.com/gmamaladze/globalmousekeyhook) |
| 데이터 저장 | System.Text.Json (팔레트 / 설정 JSON 저장)                                  |
| 배포 준비   | Costura.Fody (모든 DLL 포함하여 단일 exe 생성)                              |

## 📁 프로젝트 구조

```bash
📦 KolorPicker
├── 📄 Form1.cs             # 메인 폼 및 주요 기능 구현
├── 📄 MiniForm.cs          # 커서 근처 색상 미리보기 창
├── 📄 ZoomForm.cs          # 확대 돋보기 창
├── 📄 Program.cs           # 애플리케이션 진입점
├── 📄 packages.config      # NuGet 패키지 목록
```

### 저장 파일 구조

| 항목        | 파일 경로      | 설명                        |
| ----------- | -------------- | --------------------------- |
| 팔레트 저장 | `Palette.json` | 사용자가 저장한 팔레트 정보 |

## 🏃‍➡️ 실행 방법

1. KolorPickerSetup.zip [다운로드](https://github.com/HS-0914/KolorPicker/releases/download/v1.0/KolorPickerSetup.zip)
2. 압축 해제 후 KolorPickerSetup.exe 실행해서 KolorPicker설치
3. KolorPicker.exe 실행

## 📝 기타

- 팔레트 및 설정은 실행 파일과 같은 디렉토리의 JSON 파일로 저장됩니다.
- 전역 단축키는 프로그램 종료 시 해제되며, 프로그램은 하나의 프로그램만 실행 가능합니다.

## 📧 문의

- 손형수 (gudtn2238@gmail.com)
