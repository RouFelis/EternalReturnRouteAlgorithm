# 클론코딩 "이터널 리턴 루트 시뮬레이터"

## 개요

### 📅 제작 일자
- 2021.08

### 🎮 사용 엔진
- Unity 2020.3.17f1

## 개요
- 전공강의 "게임알고리즘" 시험(게임 내 알고리즘 간단 구현) 관련 제출 과제

- 쿼터뷰 배틀로얄 **이터널 리턴**의 **제작 및 획득 가능 아이템**에 관한  
 **루트 시뮬레이터 기능** 간단 구현

## 기능 설명
- 게임 내 16개 구역 및 600여 가지의 재료 및 조합 아이템 등의  
획득 및 제작 가능 정보 제공

- 설정된 이동 경로 구역 내 획득 및 제작 가능한 아이템에 대한 정보 제공

- 아이템에 대한 조합식은 대부분 2가지 아이템을 조합하는 이진 트리로 구성

- 본래 게임 내 기능이 아닌 외부 사이트를 통해 제공되는 기능이었으나  
게임 내 기능으로 정식 추가

## 구현 설명
- 단일 MonoBehaviour 클래스 및 UGUI로 간략하게 구현

- 실행 전 **시작 아이템 및 목표 제작 아이템** 설정 및 입력  
        - 아이템 관련 입력 및 처리는 아이템 ID 값으로 관리  

- 해당 구역의 버튼을 클릭함으로써 **경로 추가 또는 제거**

- 경로 입력 시마다 순차적으로 순회  
        - **경로 별 획득 가능 및 제작 가능 아이템** 리스트를 산출 및 텍스트 출력
