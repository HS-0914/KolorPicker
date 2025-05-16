# 🎨 KolorPicker - 색상 추출 및 팔레트 저장 도구

> **KolorPicker**는 C# WinForms로 개발된 유틸리티 앱으로, 색상 추출과 팔레트 관리를 제공합니다.

---

## 주요 기능

### ✅ 색상 추출

- 마우스 커서 위치의 색상 추출
- HEX / RGB 값 표시 및 자동 클립보드 복사
- 미리보기 패널과 작은 플로팅창을 통한 시각 피드백

### ✅ 팔레트 저장 및 관리

- HEX, RGB, 라벨 정보 저장 및 클립보드 복사
- ListView를 통한 팔레트 리스트 관리
- JSON 파일로 저장 및 불러오기

### ✅ 돋보기 (Zoom-in)

- 마우스 주변 50x50 픽셀 확대
- 확대 배율 조절(2 ~ 10)

### ✅ 전역 단축키

- `Ctrl + Shift + C`: 색상 추출 토글
- 백그라운드에서도 동작

### ✅ 기타 기능

- 토스트 메시지 피드백
- 창 닫기 후 시스템 트레이에서 조작 가능

---

## 💡 스택

| 항목        | 기술                                                                        |
| ----------- | --------------------------------------------------------------------------- |
| 플랫폼      | Windows Forms (.NET Framework)                                              |
| 언어        | C#                                                                          |
| 단축키 처리 | [Gma.System.MouseKeyHook](https://github.com/gmamaladze/globalmousekeyhook) |
| 데이터 저장 | System.Text.Json (팔레트 / 설정 JSON 저장)                                  |
| 배포 준비   | Costura.Fody (모든 DLL 포함하여 단일 exe 생성)                              |

---

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

---

## 🖼️ 주요 화면

- 색상 추출 및 확대

<img src="https://github-production-user-asset-6210df.s3.amazonaws.com/134225438/444340313-7efd0a54-59c7-4aaf-8b1d-d3703ea1adb2.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20250516%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250516T011820Z&X-Amz-Expires=300&X-Amz-Signature=65823c22aa51370cbddb31aeb5f8eedd6d3462cbc2b7a1e7a112a332658c656e&X-Amz-SignedHeaders=host" width="400" />

<br>

- 팔레트 저장 및 관리

<img src="https://github-production-user-asset-6210df.s3.amazonaws.com/134225438/444336395-0946801f-c282-4f75-a809-b886d3822e18.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20250516%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250516T005400Z&X-Amz-Expires=300&X-Amz-Signature=19e08a34898c069851cfe0d25ac99552c2fbae1353b14adda405eda03353eef8&X-Amz-SignedHeaders=host" width="400" />

<br>

- 기타 기능(시스템 트레이 실행)

<img src="https://github-production-user-asset-6210df.s3.amazonaws.com/134225438/444339623-d73b693e-12bc-4e72-915e-02ccab9a63c6.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20250516%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250516T011447Z&X-Amz-Expires=300&X-Amz-Signature=c3059b63bb119dfd511c081ba0be9c61dc6447cf709bd9f7cc941dba38a83dcb&X-Amz-SignedHeaders=host" width="400" />

## 🔧 설치 & 실행

---

## 📝 기타

- 팔레트 및 설정은 실행 파일과 같은 디렉토리의 JSON 파일로 저장됩니다.
- 전역 단축키는 프로그램 종료 시 해제되며, 프로그램은 하나의 프로그램만 실행 가능합니다.
