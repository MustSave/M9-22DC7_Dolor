# Dolor - 1:1 거대 로봇 전투 VR 게임

![프로젝트](https://private-user-images.githubusercontent.com/58774251/350707796-97138564-e6a4-4e9c-9047-355a64b5426c.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3MjE0OTQ2NzUsIm5iZiI6MTcyMTQ5NDM3NSwicGF0aCI6Ii81ODc3NDI1MS8zNTA3MDc3OTYtOTcxMzg1NjQtZTZhNC00ZTljLTkwNDctMzU1YTY0YjU0MjZjLnBuZz9YLUFtei1BbGdvcml0aG09QVdTNC1ITUFDLVNIQTI1NiZYLUFtei1DcmVkZW50aWFsPUFLSUFWQ09EWUxTQTUzUFFLNFpBJTJGMjAyNDA3MjAlMkZ1cy1lYXN0LTElMkZzMyUyRmF3czRfcmVxdWVzdCZYLUFtei1EYXRlPTIwMjQwNzIwVDE2NTI1NVomWC1BbXotRXhwaXJlcz0zMDAmWC1BbXotU2lnbmF0dXJlPTliY2U3Y2M3YjZmMGZjNDc1YjYyODYyNWE4YTA2M2I2NTc4MjZjNGQ4NGM0ZWIzZmI0OTYxOTRmZjg3ZDZkNGMmWC1BbXotU2lnbmVkSGVhZGVycz1ob3N0JmFjdG9yX2lkPTAma2V5X2lkPTAmcmVwb19pZD0wIn0.c1hAsBh15NiM3edcLfv6GgRDzq2BxKACUgfS3OgSzNU)  
**Dolor 프로젝트**는 1:1 거대 로봇 전투를 특징으로 하는 몰입형 VR 게임입니다.

## 프로젝트 개요
- **프로젝트 목적**: 1:1 거대 로봇 전투를 특징으로 하는 몰입형 VR 게임 개발.
- **개발 기간**: 2022년 6월 - 2022년 7월
- **팀 구성**: 4명

## 기술 스택
- **프로그래밍 언어**: C#
- **게임 엔진**: Unity
- **네트워킹**: Photon Network

## 씬 흐름

1. **로비 (Lobby)**:
    - **로봇 선택 (Robot Choice)**
    - **Photon 네트워크 (Photon Network)**
    - ![image](https://github.com/user-attachments/assets/86c05906-02b2-471e-a833-ef5d3e431086)

2. **연결 (Connect)**:
    - **Photon 네트워크 (Photon Network)**
    - **매칭 대기 시간 동안 연습 (Practice During Matchmaking)**: 매칭 대기 시간 동안 사용자가 연습할 수 있는 기능.
    - ![image](https://github.com/user-attachments/assets/e52f9afd-10e4-4cf8-97a3-a40dcfb62b71)

3. **인게임 (InGame)**:
    - **무기 시스템 (Weapon System)**: 다양한 무기 사용 가능. 기본 무기, 미사일, 방패 등.
    - **매칭 및 전투 (Matchmaking and Combat)**: 다른 플레이어와 매칭되어 전투를 벌이는 기능.
    - **지도 및 환경 변화 (Map and Environment Changes)**: 시간에 따른 자기장 변화를 포함한 게임 환경 변화.
    - ![6](https://github.com/user-attachments/assets/b8cdcf93-f5df-48a9-929e-789e803eeff4)

### 무기 시스템 (Weapon System)
- **기본 무기 (Basic Weapon)**: 방향에 따라 발사체를 발사하는 기본 무기.
- **미사일 (Missile)**: 조준된 적에게 유도 미사일을 발사하는 무기.
- **방패 (Shield)**: 누르고 있으면 투사체를 막는 에너지 실드를 생성하는 기능.
- **오브 A (Orb A)**: 적의 방패를 무력화시키는 오브 발사.
- **오브 B (Orb B)**: 범위 폭발을 일으키는 오브 발사.
- ![image](https://github.com/user-attachments/assets/ed07c324-19ad-477a-8935-0a0a183c0b82)
- ![image](https://github.com/user-attachments/assets/247d228e-42ab-49f7-b163-4d609c48c824)
- ![image](https://github.com/user-attachments/assets/067096ac-4edb-4402-ac60-cc923818a57d)

### 기타 기능
- **연습 모드 (RPractice Mode)**: 퀵 매치 모드와 연습 모드 제공.
- **네트워크 최적화 (Network Optimization)**: 네트워크 통신 최소화 및 중요 데이터 우선 전송으로 트래픽 감소 및 응답 속도 향상.
- 오브젝트 풀링을 통한 자원 관리 (Resource Management with Object Pooling): 게임 성능을 향상시키기 위해 오브젝트 풀링 기법을 사용하여 자원 관리.
