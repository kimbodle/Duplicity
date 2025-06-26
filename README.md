## 🎮 Duplicity

‘Duplicity(듀플리시티)’는 희귀병 치료제 연구로부터 시작된 비극을 마주한 과학자 토끼 '로라'가 진실을 밝혀나가는 생존 어드벤처 게임입니다.
연구소와 도서관, 피난처 등 여러 장소에서 퍼즐과 미션을 수행하며 로라가 연구에 얽힌 진실을 밝혀낼 수 있도록 도와주세요! 

> **Duplicity** #2D #스토리 어드벤처
플랫폼: PC / Mobile /Unity2D / Indie Game
![Unity](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white) ![Firebase](https://img.shields.io/badge/Firebase-039BE5?style=for-the-badge&logo=Firebase&logoColor=white)

## 🎥 영상
[![Duplicity Trailer](https://img.youtube.com/vi/38DB895WY1A/0.jpg)](https://youtu.be/38DB895WY1A)


---

## 📌 주요 특징
- 🧬 **서사 중심 생존 어드벤처**
  - 주인공 ‘로라’의 심리 변화와 세계관 전개
- **🔥 Firestore 기반 로그인/회원 관리** 
  - 게임 진행상황 저장 및 불러오기 가능
- **❓ 다양한 미션 시스템**
  - 매일 주어지는 미션 해결
- **📆 Day 기반 게임 구조**  
  - 추상화된 Day 데이터 관리로 엔딩 분기
- **🗨️ 인터랙션 및 다이얼로그 시스템**  
  - 캐릭터 및 오브젝트와 상호작용 가능
- **🎒 인벤토리 시스템**  
  - 아이템 습득, 사용, 조합 등 인터랙티브한 플레이


## 🛠️ 기술 스택

- **Engine**: Unity (C#)
- **Backend**: Firebase Firestore
- **Version Control**: GitHub
- **Etc.**: JSON 기반 대화 시스템, ScriptableObject 사용 구조화
  
---
### 🧩 핵심 시스템 구성

#### 📅 Day 구조 관리 (Stage 추상화)
- 각 `Day`는 **추상화된 Stage 객체**로 구성되어 있으며, 개별 이벤트/퀘스트를 동적으로 로딩합니다.
- 설계상 유연성을 확보하여 **에피소드 단위 업데이트**가 용이하도록 구성했습니다.

#### 🧠 인터랙션 시스템
- IInteractable 인터페이스를 통해, 캐릭터/오브젝트/NPC 간 상호작용 가능 요소를 통일된 방식으로 처리했습니다.
- Raycast 기반 감지 → HUD에 힌트 출력 → 상호작용 수행

#### 💬 대화 시스템 (DialogManager)
- JSON 기반 다이얼로그 데이터 관리
- 선택지 / 반응형 텍스트 연출 구현

#### 🎒 인벤토리 시스템
- 획득 아이템을 UI에 자동 배치
- 조합/사용/자세히보기 등

#### ☁️ 진행 상태 저장 & 불러오기 시스템
- Firebase Firestore를 활용한 클라우드 기반 게임 상태 저장
