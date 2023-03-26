using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public enum StartWeapon
{
    DAGGER,
    TWOHANDEDSWORD,
    AXE,
    DUALSWORDS,
    PISTOL,
    ASSAULTRIFLE,
    SNIPERRIFLE,
    RAPIER,
    SPEAR,
    HAMMER,
    BAT,
    THROW,
    SHURIKEN,
    BOW,
    CROSSBOW,
    GLOVE,
    TONFA,
    GUITAR,
    NUNCHAKU,
    WHIP,
    CAMERA
}

public class Route : MonoBehaviour
{ 
    public static List<Item> list_item = new List<Item>(); // 아이템 리스트 생성

    #region MAP

    public static List<int>[] list_map = new List<int>[]
    {
        new List<int>() { 368, 373, 377, 383, 395, 396, 404, 407, 408, 410, 419, 420, 422, 427, 429, 431, 433, 439 }, // 골목길
        new List<int>() { 371, 377, 379, 383, 384, 390, 400, 403, 405, 410, 426, 427, 429, 436, 440, 443, 444, 445 }, // 양궁장
        new List<int>() { 369, 377, 383, 385, 388, 399, 406, 409, 411, 415, 422, 426, 427, 434, 436, 437, 442, 444 }, // 번화가
        new List<int>() { 373, 374, 375, 377, 383, 395, 401, 404, 420, 425, 427, 428, 433, 438, 442, 445, 447 }, //모래사장
        new List<int>() { 374, 377, 379, 381, 383, 395, 399, 405, 412, 413, 423, 424, 427, 441, 443, 444, 446 }, // 묘지
        new List<int>() { 371, 377, 383, 385, 387, 389, 390, 391, 401, 415, 417, 418, 419, 423, 427, 440, 447 }, // 성당
        new List<int>() { 370, 371, 377, 378, 383, 385, 394, 404, 407, 409, 417, 427, 428, 429, 430, 431, 434, 444, 445 }, // 항구
        new List<int>() { 375, 377, 380, 383, 384, 386, 389, 392, 393, 420, 426, 427, 430, 431, 434, 435, 436, 439 }, // 공장
        new List<int>() { 374, 376, 377, 379, 381, 383, 384, 391, 394, 411, 412, 413, 415, 421, 422, 423, 427, 438, 441, 445 }, // 숲
        new List<int>() { 368, 377, 382, 383, 387, 396, 407, 411, 412, 424, 427, 430, 432, 435, 439, 444 }, // 병원
        new List<int>() { 370, 377, 382, 383, 388, 392, 393, 396, 402, 406, 420, 423, 424, 425, 427, 430, 437, 441, 447 }, // 호텔
        new List<int>() { 373, 374, 375, 376, 377, 378, 379, 383, 400, 408, 413, 414, 417, 421, 423, 427, 428, 438, 445, 446 }, // 연못
        new List<int>() { }, // 연구소
        new List<int>() { 368, 369, 377, 380, 383, 387, 389, 400, 401, 402, 407, 409, 414, 427, 431, 432, 435, 442, 444 }, //학교
        new List<int>() { 370, 376, 377, 378, 379, 383, 399, 403, 405, 416, 421, 427, 433, 437, 438, 440, 443 }, // 절
        new List<int>() { 369, 377, 383, 386, 393, 402, 406, 408, 410, 413, 414, 425, 427, 432, 436, 444, 447 } // 고급 주택가
    };

    public static List<string> list_mapName = new List<string>
    {
        "골목길",
        "양궁장",
        "번화가",
        "모래사장",
        "묘지",
        "성당",
        "항구",
        "공장",
        "숲",
        "병원",
        "호텔",
        "연못",
        "연구소",
        "학교",
        "절",
        "고급 주택가"
    };

    private bool[] isSelected = new bool[16] // 지역 선택여부
    { 
        false, 
        false, 
        false,
        false,
        false,
        false, 
        false, 
        false, 
        false,
        false, 
        false, 
        false, 
        false, 
        false, 
        false,
        false 
    };

    public static List<int> list_mapNum = new List<int>(); // 선택된 맵 순서 리스트

    #endregion

    public StartWeapon weapon;

    public int[] itemSlot = new int[6] // 목표 아이템 슬롯
    { 
        0, 
        0,
        0,
        0,
        0, 
        0 
    };

    public static List<int> list_itemGuide = new List<int>(); // 제작 목표 아이템 리스트
    public static List<int> list_inventory = new List<int>(); // 인벤토리 리스트

    public List<string> list_RouteInfo = new List<string>();

    public Text[] txtMapNum = new Text[16];
    public Text txtRouteInfo;
    public Text txtMapMaterial;

    void Awake()
    {
        // 아이템 리스트 (전체 614개 중 '특수 재료 아이템과 아이템 제작에 관계없는 아이템을 제외'한 448개 적용)
        // list_item.Add(new Item(INDEX, NAME, ITEMTYPE, QUANTITY, L_NODE, R_NODE)); 아이템 데이터 추가 방식
        #region LIST_ITEM

        list_item.Add(new Item(0, "하르페", "장비", 1, 155, 251));
        list_item.Add(new Item(1, "아그니", "장비", 1, 78, 348));
        list_item.Add(new Item(2, "미스틸테인", "장비", 1, 89, 377));
        list_item.Add(new Item(3, "레이더", "장비", 1, 125, 263));
        list_item.Add(new Item(4, "EOD 부츠", "장비", 1, 328, 241));
        list_item.Add(new Item(5, "카른웬난", "장비", 1, 139, 330));
        list_item.Add(new Item(6, "파산검", "장비", 1, 139, 351));
        list_item.Add(new Item(7, "초진동나이프", "장비", 1, 269, 262));
        list_item.Add(new Item(8, "다마스커스 가시", "장비", 1, 140, 360));
        list_item.Add(new Item(9, "마하라자", "장비", 1, 141, 255));
        list_item.Add(new Item(10, "뚜언 띠엔", "장비", 1, 145, 311));
        list_item.Add(new Item(11, "아론다이트", "장비", 1, 143, 419));
        list_item.Add(new Item(12, "엑스칼리버", "장비", 1, 146, 418));
        list_item.Add(new Item(13, "모노호시자오", "장비", 1, 144, 353));
        list_item.Add(new Item(14, "호푸어드", "장비", 1, 146, 332));
        list_item.Add(new Item(15, "이천일류", "장비", 1, 148, 360));
        list_item.Add(new Item(16, "아수라", "장비", 1, 148, 335));
        list_item.Add(new Item(17, "디오스쿠로이", "장비", 1, 273, 264));
        list_item.Add(new Item(18, "로이거 차르", "장비", 1, 149, 351));
        list_item.Add(new Item(19, "낭아봉", "장비", 1, 150, 356));
        list_item.Add(new Item(20, "다그다의 망치", "장비", 1, 151, 330));
        list_item.Add(new Item(21, "토르의 망치", "장비", 1, 151, 264));
        list_item.Add(new Item(22, "천근추", "장비", 1, 152, 335));
        list_item.Add(new Item(23, "금강저", "장비", 1, 163, 263));
        list_item.Add(new Item(24, "빔 엑스", "장비", 1, 155, 432));
        list_item.Add(new Item(25, "산타 무에르테", "장비", 1, 154, 358));
        list_item.Add(new Item(26, "스퀴테", "장비", 1, 154, 355));
        list_item.Add(new Item(27, "파라슈", "장비", 1, 153, 335));
        list_item.Add(new Item(28, "저거너트", "장비", 1, 153, 262));
        list_item.Add(new Item(29, "애각창", "장비", 1, 159, 350));
        list_item.Add(new Item(30, "장팔사모", "장비", 1, 159, 264));
        list_item.Add(new Item(31, "트리아이나", "장비", 1, 156, 157));
        list_item.Add(new Item(32, "방천화극", "장비", 1, 158, 338));
        list_item.Add(new Item(33, "청룡언월도", "장비", 1, 158, 354));
        list_item.Add(new Item(34, "구원의 여신상", "장비", 1, 162, 333));
        list_item.Add(new Item(35, "타구봉", "장비", 1, 160, 352));
        list_item.Add(new Item(36, "스파이의 우산", "장비", 1, 161, 351));
        list_item.Add(new Item(37, "뇌룡편", "장비", 1, 165, 355));
        list_item.Add(new Item(38, "글레이프니르", "장비", 1, 164, 360));
        list_item.Add(new Item(39, "플라즈마 윕", "장비", 1, 165, 432));
        list_item.Add(new Item(40, "단영촌천투", "장비", 1, 168, 333));
        list_item.Add(new Item(41, "디바인 피스트", "장비", 1, 169, 419));
        list_item.Add(new Item(42, "블러드윙 너클", "장비", 1, 167, 358));
        list_item.Add(new Item(43, "빙화현옥수", "장비", 1, 171, 424));
        list_item.Add(new Item(44, "여래수투", "장비", 1, 171, 335));
        list_item.Add(new Item(45, "브레이질 건틀릿", "장비", 1, 168, 347));
        list_item.Add(new Item(46, "소수", "장비", 1, 170, 350));
        list_item.Add(new Item(47, "택티컬 톤파", "장비", 1, 173, 353));
        list_item.Add(new Item(48, "마이쏙", "장비", 1, 174, 378));
        list_item.Add(new Item(49, "플라즈마 톤파", "장비", 1, 174, 433));
        list_item.Add(new Item(50, "소이탄", "장비", 1, 285, 177));
        list_item.Add(new Item(51, "안티오크의 수류탄", "장비", 1, 54, 419));
        list_item.Add(new Item(52, "다비드슬링", "장비", 1, 175, 330));
        list_item.Add(new Item(53, "연막탄", "장비", 1, 176, 345));
        list_item.Add(new Item(54, "고폭 수류탄", "장비", 1, 284, 266));
        list_item.Add(new Item(55, "파이어 볼", "장비", 1, 286, 268));
        list_item.Add(new Item(56, "아스트라페", "장비", 1, 178, 332));
        list_item.Add(new Item(57, "루테늄 구슬", "장비", 1, 179, 355));
        list_item.Add(new Item(58, "미치광이왕의 카드", "장비", 1, 288, 264));
        list_item.Add(new Item(59, "옥전결", "장비", 1, 180, 355));
        list_item.Add(new Item(60, "풍마 수리검", "장비", 1, 181, 435));
        list_item.Add(new Item(61, "푸른색 단도", "장비", 1, 183, 261));
        list_item.Add(new Item(62, "플레솃", "장비", 1, 186, 350));
        list_item.Add(new Item(63, "건곤권", "장비", 1, 185, 379));
        list_item.Add(new Item(64, "편전", "장비", 1, 189, 379));
        list_item.Add(new Item(65, "골든래쇼 보우", "장비", 1, 191, 355));
        list_item.Add(new Item(66, "트윈보우", "장비", 1, 188, 187));
        list_item.Add(new Item(67, "제베의 활", "장비", 1, 190, 247));
        list_item.Add(new Item(68, "엘리멘탈 보우", "장비", 1, 192, 244));
        list_item.Add(new Item(69, "대황", "장비", 1, 196, 347));
        list_item.Add(new Item(70, "발리스타", "장비", 1, 195, 376));
        list_item.Add(new Item(71, "저격 크로스보우", "장비", 1, 194, 334));
        list_item.Add(new Item(72, "영광금기신기노", "장비", 1, 193, 266));
        list_item.Add(new Item(73, "엘레강스", "장비", 1, 197, 337));
        list_item.Add(new Item(74, "일렉트론 블라스터", "장비", 1, 296, 264));
        list_item.Add(new Item(75, "매그넘-보아", "장비", 1, 199, 346));
        list_item.Add(new Item(76, "글록 48", "장비", 1, 197, 267));
        list_item.Add(new Item(77, "스탬피드", "장비", 1, 200, 355));
        list_item.Add(new Item(78, "개틀링 건", "장비", 1, 297, 262));
        list_item.Add(new Item(79, "95식 자동 소총", "장비", 1, 201, 346));
        list_item.Add(new Item(80, "AK-12", "장비", 1, 201, 263));
        list_item.Add(new Item(81, "XCR", "장비", 1, 202, 246));
        list_item.Add(new Item(82, "Tac-50", "장비", 1, 203, 353));
        list_item.Add(new Item(83, "인터벤션", "장비", 1, 203, 364));
        list_item.Add(new Item(84, "NTW-20", "장비", 1, 204, 354));
        list_item.Add(new Item(85, "폴라리스", "장비", 1, 205, 350));
        list_item.Add(new Item(86, "대소반곤룡", "장비", 1, 206, 351));
        list_item.Add(new Item(87, "초진동눈차크", "장비", 1, 207, 262));
        list_item.Add(new Item(88, "케르베로스", "장비", 1, 206, 323));
        list_item.Add(new Item(89, "활빈검", "장비", 1, 300, 234));
        list_item.Add(new Item(90, "듀랜달 MK2", "장비", 1, 208, 431));
        list_item.Add(new Item(91, "볼틱레토", "장비", 1, 208, 352));
        list_item.Add(new Item(92, "레드 팬서", "장비", 1, 209, 238));
        list_item.Add(new Item(93, "보헤미안", "장비", 1, 210, 388));
        list_item.Add(new Item(94, "천국의 계단", "장비", 1, 211, 418));
        list_item.Add(new Item(95, "퍼플 헤이즈", "장비", 1, 212, 351));
        list_item.Add(new Item(96, "새티스팩션", "장비", 1, 213, 389));
        list_item.Add(new Item(97, "더 월", "장비", 1, 214, 350));
        list_item.Add(new Item(98, "틴 스피릿", "장비", 1, 215, 335));
        list_item.Add(new Item(99, "미러리스", "장비", 1, 216, 355));
        list_item.Add(new Item(100, "컴파운드 사이트", "장비", 1, 217, 413));
        list_item.Add(new Item(101, "카메라 캐논", "장비", 1, 218, 246));
        list_item.Add(new Item(102, "V.I.C.G", "장비", 1, 303, 265));
        list_item.Add(new Item(103, "수정 티아라", "장비", 1, 221, 332));
        list_item.Add(new Item(104, "전술-OPS 헬멧", "장비", 1, 219, 352));
        list_item.Add(new Item(105, "기사단장의 투구", "장비", 1, 223, 429));
        list_item.Add(new Item(106, "제국 왕관", "장비", 1, 222, 358));
        list_item.Add(new Item(107, "황실 부르고넷", "장비", 1, 223, 355));
        list_item.Add(new Item(108, "모호크 헬멧", "장비", 1, 220, 360));
        list_item.Add(new Item(109, "비질란테", "장비", 1, 220, 361));
        list_item.Add(new Item(110, "다이아뎀", "장비", 1, 305, 239));
        list_item.Add(new Item(111, "광학미채 슈트", "장비", 1, 316, 263));
        list_item.Add(new Item(112, "락커의 자켓", "장비", 1, 225, 363));
        list_item.Add(new Item(113, "성기사의 갑옷", "장비", 1, 229, 330));
        list_item.Add(new Item(114, "아마조네스 아머", "장비", 1, 229, 315));
        list_item.Add(new Item(115, "용의 도복", "장비", 1, 228, 311));
        list_item.Add(new Item(116, "지휘관의 갑옷", "장비", 1, 226, 355));
        list_item.Add(new Item(117, "집사복", "장비", 1, 227, 337));
        list_item.Add(new Item(118, "배틀 슈트", "장비", 1, 232, 316));
        list_item.Add(new Item(119, "EOD 슈트", "장비", 1, 232, 313));
        list_item.Add(new Item(120, "턱시도", "장비", 1, 227, 314));
        list_item.Add(new Item(121, "제사장의 예복", "장비", 1, 230, 260));
        list_item.Add(new Item(122, "창파오", "장비", 1, 228, 259));
        list_item.Add(new Item(123, "소드 스토퍼", "장비", 1, 237, 426));
        list_item.Add(new Item(124, "드라우프니르", "장비", 1, 236, 320));
        list_item.Add(new Item(125, "바이탈 센서", "장비", 1, 406, 265));
        list_item.Add(new Item(126, "기사의 신조", "장비", 1, 240, 319));
        list_item.Add(new Item(127, "샤자한의 검집", "장비", 1, 235, 358));
        list_item.Add(new Item(128, "아이기스", "장비", 1, 238, 318));
        list_item.Add(new Item(129, "틴달로스의 팔찌", "장비", 1, 236, 321));
        list_item.Add(new Item(130, "나이팅게일", "장비", 1, 319, 258));
        list_item.Add(new Item(131, "플라즈마 아크", "장비", 1, 240, 352));
        list_item.Add(new Item(132, "스마트 밴드", "장비", 1, 239, 353));
        list_item.Add(new Item(133, "경량화 부츠", "장비", 1, 242, 412));
        list_item.Add(new Item(134, "매버릭 러너", "장비", 1, 325, 265));
        list_item.Add(new Item(135, "풍화륜", "장비", 1, 327, 351));
        list_item.Add(new Item(136, "부케팔로스", "장비", 1, 243, 323));
        list_item.Add(new Item(137, "클링온 부츠", "장비", 1, 242, 426));
        list_item.Add(new Item(138, "타키온 브레이스", "장비", 1, 322, 262));
        list_item.Add(new Item(139, "장미칼", "장비", 1, 269, 413));
        list_item.Add(new Item(140, "스위스 아미 나이프", "장비", 1, 270, 368));
        list_item.Add(new Item(141, "카라페이스 카타르", "장비", 1, 271, 428));
        list_item.Add(new Item(142, "일본도", "장비", 1, 371, 354));
        list_item.Add(new Item(143, "마사무네", "장비", 1, 142, 436));
        list_item.Add(new Item(144, "무라마사", "장비", 1, 142, 438));
        list_item.Add(new Item(145, "바스타드 소드", "장비", 1, 371, 346));
        list_item.Add(new Item(146, "보검", "장비", 1, 272, 358));
        list_item.Add(new Item(147, "플라즈마 소드", "장비", 1, 145, 432));
        list_item.Add(new Item(148, "피렌체식 쌍검", "장비", 1, 372, 353));
        list_item.Add(new Item(149, "쌍둥이 검", "장비", 1, 372, 235));
        list_item.Add(new Item(150, "모닝 스타", "장비", 1, 274, 384));
        list_item.Add(new Item(151, "사슴 망치", "장비", 1, 274, 427));
        list_item.Add(new Item(152, "운명의 망치", "장비", 1, 373, 331));
        list_item.Add(new Item(153, "경량화 도끼", "장비", 1, 276, 412));
        list_item.Add(new Item(154, "사신의 낫", "장비", 1, 275, 378));
        list_item.Add(new Item(155, "대부", "장비", 1, 276, 346));
        list_item.Add(new Item(156, "바이던트", "장비", 1, 277, 370));
        list_item.Add(new Item(157, "파이크", "장비", 1, 376, 346));
        list_item.Add(new Item(158, "도끼창", "장비", 1, 375, 157));
        list_item.Add(new Item(159, "강창", "장비", 1, 277, 412));
        list_item.Add(new Item(160, "도깨비 방망이", "장비", 1, 278, 426));
        list_item.Add(new Item(161, "우산", "장비", 1, 278, 415));
        list_item.Add(new Item(162, "횃불", "장비", 1, 378, 347));
        list_item.Add(new Item(163, "몽둥이", "장비", 1, 378, 346));
        list_item.Add(new Item(164, "바람 채찍", "장비", 1, 279, 412));
        list_item.Add(new Item(165, "벽력편", "장비", 1, 280, 349));
        list_item.Add(new Item(166, "건틀릿", "장비", 1, 382, 346));
        list_item.Add(new Item(167, "윙 너클", "장비", 1, 282, 412));
        list_item.Add(new Item(168, "귀골 장갑", "장비", 1, 166, 428));
        list_item.Add(new Item(169, "벽력귀투", "장비", 1, 166, 443));
        list_item.Add(new Item(170, "유리 너클", "장비", 1, 282, 332));
        list_item.Add(new Item(171, "회단 장갑", "장비", 1, 281, 351));
        list_item.Add(new Item(172, "경찰봉", "장비", 1, 283, 433));
        list_item.Add(new Item(173, "류큐톤파", "장비", 1, 283, 350));
        list_item.Add(new Item(174, "슬링", "장비", 1, 286, 429));
        list_item.Add(new Item(175, "밀가루 폭탄", "장비", 1, 350, 285));
        list_item.Add(new Item(176, "볼 라이트닝", "장비", 1, 384, 349));
        list_item.Add(new Item(177, "플러버", "장비", 1, 429, 343));
        list_item.Add(new Item(178, "필럼", "장비", 1, 376, 341));
        list_item.Add(new Item(179, "가시 탱탱볼", "장비", 1, 177, 426));
        list_item.Add(new Item(180, "부적", "장비", 1, 288, 389));
        list_item.Add(new Item(181, "유엽비도", "장비", 1, 183, 377));
        list_item.Add(new Item(182, "챠크람", "장비", 1, 289, 433));
        list_item.Add(new Item(183, "매화비표", "장비", 1, 290, 413));
        list_item.Add(new Item(184, "독침", "장비", 1, 396, 261));
        list_item.Add(new Item(185, "법륜", "장비", 1, 182, 416));
        list_item.Add(new Item(186, "플럼바타", "장비", 1, 287, 346));
        list_item.Add(new Item(187, "컴포지드 보우", "장비", 1, 292, 426));
        list_item.Add(new Item(188, "강궁", "장비", 1, 291, 436));
        list_item.Add(new Item(189, "국궁", "장비", 1, 190, 433));
        list_item.Add(new Item(190, "벽력궁", "장비", 1, 292, 443));
        list_item.Add(new Item(191, "탄궁", "장비", 1, 291, 356));
        list_item.Add(new Item(192, "화전", "장비", 1, 292, 431));
        list_item.Add(new Item(193, "노", "장비", 1, 293, 429));
        list_item.Add(new Item(194, "저격궁", "장비", 1, 294, 432));
        list_item.Add(new Item(195, "헤비 크로스보우", "장비", 1, 293, 346));
        list_item.Add(new Item(196, "철궁", "장비", 1, 294, 354));
        list_item.Add(new Item(197, "FN57", "장비", 1, 296, 432));
        list_item.Add(new Item(198, "더블 리볼버 SP", "장비", 1, 295, 392));
        list_item.Add(new Item(199, "매그넘-아나콘다", "장비", 1, 295, 353));
        list_item.Add(new Item(200, "데린저", "장비", 1, 392, 362));
        list_item.Add(new Item(201, "AK-47", "장비", 1, 297, 447));
        list_item.Add(new Item(202, "M16A1", "장비", 1, 297, 427));
        list_item.Add(new Item(203, "하푼건", "장비", 1, 298, 376));
        list_item.Add(new Item(204, "금교전", "장비", 1, 298, 355));
        list_item.Add(new Item(205, "레일건", "장비", 1, 298, 352));
        list_item.Add(new Item(206, "샤퍼", "장비", 1, 309, 426));
        list_item.Add(new Item(207, "블리더", "장비", 1, 309, 387));
        list_item.Add(new Item(208, "매화검", "장비", 1, 300, 331));
        list_item.Add(new Item(209, "에스톡", "장비", 1, 396, 300));
        list_item.Add(new Item(210, "루비 스페셜", "장비", 1, 302, 358));
        list_item.Add(new Item(211, "험버커 픽업", "장비", 1, 301, 334));
        list_item.Add(new Item(212, "King-V", "장비", 1, 301, 368));
        list_item.Add(new Item(213, "노캐스터", "장비", 1, 302, 353));
        list_item.Add(new Item(214, "슈퍼스트랫", "장비", 1, 302, 379));
        list_item.Add(new Item(215, "야생마", "장비", 1, 302, 436));
        list_item.Add(new Item(216, "컴팩트 카메라", "장비", 1, 366, 398));
        list_item.Add(new Item(217, "레인지파인더", "장비", 1, 334, 321));
        list_item.Add(new Item(218, "카메라 라이플", "장비", 1, 303, 393));
        list_item.Add(new Item(219, "방탄모", "장비", 1, 306, 401));
        list_item.Add(new Item(220, "소방 헬멧", "장비", 1, 308, 343));
        list_item.Add(new Item(221, "티아라", "장비", 1, 305, 433));
        list_item.Add(new Item(222, "왕관", "장비", 1, 306, 355));
        list_item.Add(new Item(223, "투구", "장비", 1, 307, 304));
        list_item.Add(new Item(224, "오토바이 헬멧", "장비", 1, 308, 334));
        list_item.Add(new Item(225, "라이더 자켓", "장비", 1, 310, 395));
        list_item.Add(new Item(226, "사슬 갑옷", "장비", 1, 309, 395));
        list_item.Add(new Item(227, "정장", "장비", 1, 310, 414));
        list_item.Add(new Item(228, "치파오", "장비", 1, 314, 387));
        list_item.Add(new Item(229, "판금 갑옷", "장비", 1, 405, 346));
        list_item.Add(new Item(230, "한복", "장비", 1, 313, 413));
        list_item.Add(new Item(231, "고위 사제복", "장비", 1, 317, 347));
        list_item.Add(new Item(232, "방탄조끼", "장비", 1, 312, 354));
        list_item.Add(new Item(233, "석양의 갑옷", "장비", 1, 309, 358));
        list_item.Add(new Item(234, "어사의", "장비", 1, 230, 433));
        list_item.Add(new Item(235, "검집", "장비", 1, 427, 354));
        list_item.Add(new Item(236, "금팔찌", "장비", 1, 408, 355));
        list_item.Add(new Item(237, "바주반드", "장비", 1, 320, 354));
        list_item.Add(new Item(238, "진홍 팔찌", "장비", 1, 408, 360));
        list_item.Add(new Item(239, "바브드 블로썸", "장비", 1, 413, 357));
        list_item.Add(new Item(240, "강철 방패", "장비", 1, 318, 346));
        list_item.Add(new Item(241, "강철 무릎 보호대", "장비", 1, 322, 346));
        list_item.Add(new Item(242, "전투화", "장비", 1, 328, 427));
        list_item.Add(new Item(243, "킬힐", "장비", 1, 324, 332));
        list_item.Add(new Item(244, "백우선", "장비", 1, 338, 412));
        list_item.Add(new Item(245, "우치와", "장비", 1, 333, 415));
        list_item.Add(new Item(246, "탄창", "장비", 1, 417, 354));
        list_item.Add(new Item(247, "궁기병의 화살통", "장비", 1, 336, 337));
        list_item.Add(new Item(248, "월왕구천", "장비", 1, 339, 353));
        list_item.Add(new Item(249, "해적의 증표", "장비", 1, 340, 370));
        list_item.Add(new Item(250, "호크 아이", "장비", 1, 341, 334));
        list_item.Add(new Item(251, "해적 깃발", "장비", 1, 337, 340));
        list_item.Add(new Item(252, "오르골", "장비", 1, 417, 262));
        list_item.Add(new Item(253, "능동 위장", "장비", 1, 246, 312));
        list_item.Add(new Item(254, "마도서", "장비", 1, 359, 331));
        list_item.Add(new Item(255, "아이테르 깃발", "장비", 1, 412, 331));
        list_item.Add(new Item(256, "슈뢰딩거의 상자", "장비", 1, 417, 261));
        list_item.Add(new Item(257, "진리는 나의 빛", "장비", 1, 335, 330));
        list_item.Add(new Item(258, "힐링 포션", "재료", 2, 342, 385));
        list_item.Add(new Item(259, "백일취", "재료", 5, 344, 413));
        list_item.Add(new Item(260, "정화수", "재료", 2, 343, 424));
        list_item.Add(new Item(261, "독약", "재료", 1, 351, 423));
        list_item.Add(new Item(262, "모터", "재료", 1, 352, 430));
        list_item.Add(new Item(263, "유리판", "재료", 1, 332, 439));
        list_item.Add(new Item(264, "이온 전지", "재료", 1, 349, 425));
        list_item.Add(new Item(265, "휴대폰", "재료", 1, 353, 352));
        list_item.Add(new Item(266, "RDX", "재료", 1, 362, 430));
        list_item.Add(new Item(267, "EMP 드론", "재료", 2, 365, 434));
        list_item.Add(new Item(268, "화염 트랩", "재료", 1, 367, 348));
        list_item.Add(new Item(269, "군용 나이프", "장비", 1, 370, 377));
        list_item.Add(new Item(270, "메스", "장비", 1, 369, 387));
        list_item.Add(new Item(271, "자마다르", "장비", 1, 370, 381));
        list_item.Add(new Item(272, "샴쉬르", "장비", 1, 371, 431));
        list_item.Add(new Item(273, "조잡한 쌍검", "장비", 1, 368, 374));
        list_item.Add(new Item(274, "워해머", "장비", 1, 373, 378));
        list_item.Add(new Item(275, "사슬 낫", "장비", 1, 374, 395));
        list_item.Add(new Item(276, "전투 도끼", "장비", 1, 375, 379));
        list_item.Add(new Item(277, "죽창", "장비", 1, 376, 379));
        list_item.Add(new Item(278, "장봉", "장비", 1, 378, 379));
        list_item.Add(new Item(279, "오랏줄", "장비", 1, 380, 433));
        list_item.Add(new Item(280, "철편", "장비", 1, 380, 387));
        list_item.Add(new Item(281, "글러브", "장비", 1, 382, 427));
        list_item.Add(new Item(282, "아이언 너클", "장비", 1, 381, 441));
        list_item.Add(new Item(283, "톤파", "장비", 1, 379, 377));
        list_item.Add(new Item(284, "수류탄", "장비", 1, 384, 443));
        list_item.Add(new Item(285, "화염병", "장비", 1, 385, 436));
        list_item.Add(new Item(286, "싸인볼", "장비", 1, 386, 369));
        list_item.Add(new Item(287, "다트", "장비", 1, 396, 412));
        list_item.Add(new Item(288, "빈티지 카드", "장비", 1, 388, 369));
        list_item.Add(new Item(289, "표창", "장비", 1, 387, 447));
        list_item.Add(new Item(290, "흑건", "장비", 1, 387, 419));
        list_item.Add(new Item(291, "목궁", "장비", 1, 390, 447));
        list_item.Add(new Item(292, "장궁", "장비", 1, 390, 429));
        list_item.Add(new Item(293, "쇠뇌", "장비", 1, 391, 447));
        list_item.Add(new Item(294, "크로스보우", "장비", 1, 391, 379));
        list_item.Add(new Item(295, "매그넘-파이썬", "장비", 1, 392, 436));
        list_item.Add(new Item(296, "베레타 M92F", "장비", 1, 392, 427));
        list_item.Add(new Item(297, "STG-44", "장비", 1, 393, 443));
        list_item.Add(new Item(298, "스프링필드", "장비", 1, 394, 432));
        list_item.Add(new Item(299, "눈차크", "장비", 1, 395, 430));
        list_item.Add(new Item(300, "레이피어", "장비", 1, 396, 441));
        list_item.Add(new Item(301, "골든 브릿지", "장비", 1, 397, 355));
        list_item.Add(new Item(302, "싱글 픽업", "장비", 1, 397, 349));
        list_item.Add(new Item(303, "카메라 건", "장비", 1, 398, 392));
        list_item.Add(new Item(304, "가면", "장비", 1, 399, 412));
        list_item.Add(new Item(305, "머리테", "장비", 1, 399, 377));
        list_item.Add(new Item(306, "베레모", "장비", 1, 400, 368));
        list_item.Add(new Item(307, "사슬 코이프", "장비", 1, 400, 395));
        list_item.Add(new Item(308, "안전모", "장비", 1, 401, 383));
        list_item.Add(new Item(309, "가죽 갑옷", "장비", 1, 405, 427));
        list_item.Add(new Item(310, "가죽 자켓", "장비", 1, 402, 427));
        list_item.Add(new Item(311, "거북 도복", "장비", 1, 403, 428));
        list_item.Add(new Item(312, "군복", "장비", 1, 402, 377));
        list_item.Add(new Item(313, "덧댄 로브", "장비", 1, 403, 407));
        list_item.Add(new Item(314, "드레스", "장비", 1, 437, 368));
        list_item.Add(new Item(315, "비키니", "장비", 1, 404, 368));
        list_item.Add(new Item(316, "잠수복", "장비", 1, 404, 429));
        list_item.Add(new Item(317, "사제복", "장비", 1, 419, 437));
        list_item.Add(new Item(318, "가죽 방패", "장비", 1, 428, 427));
        list_item.Add(new Item(319, "분대장 완장", "장비", 1, 396, 407));
        list_item.Add(new Item(320, "브레이서", "장비", 1, 407, 427));
        list_item.Add(new Item(321, "고장난 시계", "장비", 1, 406, 439));
        list_item.Add(new Item(322, "무릎 보호대", "장비", 1, 411, 439));
        list_item.Add(new Item(323, "체인 레깅스", "장비", 1, 411, 395));
        list_item.Add(new Item(324, "하이힐", "장비", 1, 409, 430));
        list_item.Add(new Item(325, "힐리스", "장비", 1, 410, 384));
        list_item.Add(new Item(326, "나막신", "장비", 1, 377, 445));
        list_item.Add(new Item(327, "덧댄 슬리퍼", "장비", 1, 409, 437));
        list_item.Add(new Item(328, "부츠", "장비", 1, 410, 347));
        list_item.Add(new Item(329, "등산화", "장비", 1, 326, 383));
        list_item.Add(new Item(330, "성자의 유산", "장비", 1, 419, 418));
        list_item.Add(new Item(331, "운명의 꽃", "장비", 1, 413, 388));
        list_item.Add(new Item(332, "유리 조각", "장비", 1, 385, 383));
        list_item.Add(new Item(333, "인형", "장비", 1, 414, 437));
        list_item.Add(new Item(334, "저격 스코프", "장비", 1, 432, 420));
        list_item.Add(new Item(335, "진신사리", "장비", 1, 416, 403));
        list_item.Add(new Item(336, "화살통", "장비", 1, 427, 379));
        list_item.Add(new Item(337, "먼지털이개", "장비", 1, 378, 412));
        list_item.Add(new Item(338, "군선", "장비", 1, 415, 426));
        list_item.Add(new Item(339, "비파단도", "장비", 1, 371, 433));
        list_item.Add(new Item(340, "캐리비안 장식총", "장비", 1, 392, 414));
        list_item.Add(new Item(341, "사격 교본", "장비", 1, 391, 393));
        list_item.Add(new Item(342, "난초", "재료", 2, 421, 413));
        list_item.Add(new Item(343, "뜨거운 물", "재료", 3, 423, 431));
        list_item.Add(new Item(344, "백주", "재료", 1, 435, 385));
        list_item.Add(new Item(345, "콜라", "재료", 3, 425, 422));
        list_item.Add(new Item(346, "강철", "재료", 2, 430, 441));
        list_item.Add(new Item(347, "기름먹인 천", "재료", 2, 436, 407));
        list_item.Add(new Item(348, "뜨거운 오일", "재료", 3, 436, 431));
        list_item.Add(new Item(349, "방전 전지", "재료", 1, 434, 423));
        list_item.Add(new Item(350, "백색 가루", "재료", 1, 389, 383));
        list_item.Add(new Item(351, "재", "재료", 1, 440, 431));
        list_item.Add(new Item(352, "전자 부품", "재료", 1, 434, 447));
        list_item.Add(new Item(353, "정교한 도면", "재료", 1, 369, 440));
        list_item.Add(new Item(354, "철판", "재료", 2, 430, 373));
        list_item.Add(new Item(355, "황금", "재료", 1, 374, 438));
        list_item.Add(new Item(356, "달궈진 돌멩이", "재료", 3, 383, 431));
        list_item.Add(new Item(357, "철사", "재료", 1, 447, 373));
        list_item.Add(new Item(358, "루비", "재료", 1, 373, 438));
        list_item.Add(new Item(359, "하드커버", "재료", 1, 440, 427));
        list_item.Add(new Item(360, "가시 발판", "재료", 2, 446, 426));
        list_item.Add(new Item(361, "개량형 쥐덫", "재료", 2, 446, 441));
        list_item.Add(new Item(362, "다이너마이트", "재료", 1, 447, 443));
        list_item.Add(new Item(363, "소란 발생기", "재료", 3, 442, 384));
        list_item.Add(new Item(364, "망원 카메라", "재료", 3, 444, 420));
        list_item.Add(new Item(365, "정찰 드론", "재료", 3, 444, 443));
        list_item.Add(new Item(366, "위장 카메라", "재료", 1, 444, 377));
        list_item.Add(new Item(367, "폭발 트랩", "재료", 1, 446, 443));
        list_item.Add(new Item(368, "가위", "장비", 1, -1, -1));
        list_item.Add(new Item(369, "만년필", "장비", 1, -1, -1));
        list_item.Add(new Item(370, "식칼", "장비", 1, -1, -1));
        list_item.Add(new Item(371, "녹슨 검", "장비", 1, -1, -1));
        list_item.Add(new Item(372, "쌍칼", "장비", 1, 369, 370));
        list_item.Add(new Item(373, "망치", "장비", 1, -1, -1));
        list_item.Add(new Item(374, "곡괭이", "장비", 1, -1, -1));
        list_item.Add(new Item(375, "손도끼", "장비", 1, -1, -1));
        list_item.Add(new Item(376, "단창", "장비", 1, -1, -1));
        list_item.Add(new Item(377, "나뭇가지", "장비", 1, -1, -1));
        list_item.Add(new Item(378, "단봉", "장비", 1, -1, -1));
        list_item.Add(new Item(379, "대나무", "장비", 1, -1, -1));
        list_item.Add(new Item(380, "채찍", "장비", 1, -1, -1));
        list_item.Add(new Item(381, "너클", "장비", 1, -1, -1));
        list_item.Add(new Item(382, "목장갑", "장비", 1, -1, -1));
        list_item.Add(new Item(383, "돌멩이", "장비", 2, -1, -1));
        list_item.Add(new Item(384, "쇠구슬", "장비", 1, -1, -1));
        list_item.Add(new Item(385, "유리병", "장비", 2, -1, -1));
        list_item.Add(new Item(386, "야구공", "장비", 1, -1, -1));
        list_item.Add(new Item(387, "면도칼", "장비", 1, -1, -1));
        list_item.Add(new Item(388, "트럼프 카드", "장비", 1, -1, -1));
        list_item.Add(new Item(389, "분필", "장비", 1, -1, -1));
        list_item.Add(new Item(390, "양궁", "장비", 1, -1, -1));
        list_item.Add(new Item(391, "석궁", "장비", 1, -1, -1));
        list_item.Add(new Item(392, "발터 PPK", "장비", 1, -1, -1));
        list_item.Add(new Item(393, "페도로프 자동소총", "장비", 1, -1, -1));
        list_item.Add(new Item(394, "화승총", "장비", 1, -1, -1));
        list_item.Add(new Item(395, "쇠사슬", "장비", 1, -1, -1));
        list_item.Add(new Item(396, "바늘", "장비", 1, -1, -1));
        list_item.Add(new Item(397, "보급형 기타", "장비", 1, 379, 447));
        list_item.Add(new Item(398, "렌즈", "장비", 1, 385, 444));
        list_item.Add(new Item(399, "머리띠", "장비", 1, -1, -1));
        list_item.Add(new Item(400, "모자", "장비", 1, -1, -1));
        list_item.Add(new Item(401, "자전거 헬멧", "장비", 1, -1, -1));
        list_item.Add(new Item(402, "바람막이", "장비", 1, -1, -1));
        list_item.Add(new Item(403, "승복", "장비", 1, -1, -1));
        list_item.Add(new Item(404, "전신 수영복", "장비", 1, -1, -1));
        list_item.Add(new Item(405, "천 갑옷", "장비", 1, -1, -1));
        list_item.Add(new Item(406, "손목시계", "장비", 1, -1, -1));
        list_item.Add(new Item(407, "붕대", "장비", 1, -1, -1));
        list_item.Add(new Item(408, "팔찌", "장비", 1, -1, -1));
        list_item.Add(new Item(409, "슬리퍼", "장비", 1, -1, -1));
        list_item.Add(new Item(410, "운동화", "장비", 1, -1, -1));
        list_item.Add(new Item(411, "타이즈", "장비", 1, -1, -1));
        list_item.Add(new Item(412, "깃털", "장비", 1, -1, -1));
        list_item.Add(new Item(413, "꽃", "장비", 1, -1, -1));
        list_item.Add(new Item(414, "리본", "장비", 1, -1, -1));
        list_item.Add(new Item(415, "부채", "장비", 1, -1, -1));
        list_item.Add(new Item(416, "불경", "장비", 1, -1, -1));
        list_item.Add(new Item(417, "상자", "장비", 1, -1, -1));
        list_item.Add(new Item(418, "성배", "장비", 1, -1, -1));
        list_item.Add(new Item(419, "십자가", "장비", 1, -1, -1));
        list_item.Add(new Item(420, "쌍안경", "장비", 1, -1, -1));
        list_item.Add(new Item(421, "약초", "재료", 1, -1, -1));
        list_item.Add(new Item(422, "꿀", "재료", 1, -1, -1));
        list_item.Add(new Item(423, "물", "재료", 3, -1, -1));
        list_item.Add(new Item(424, "얼음", "재료", 2, -1, -1));
        list_item.Add(new Item(425, "탄산수", "재료", 1, -1, -1));
        list_item.Add(new Item(426, "못", "재료", 1, -1, -1));
        list_item.Add(new Item(427, "가죽", "재료", 1, -1, -1));
        list_item.Add(new Item(428, "거북이 등딱지", "재료", 1, -1, -1));
        list_item.Add(new Item(429, "고무", "재료", 1, -1, -1));
        list_item.Add(new Item(430, "고철", "재료", 1, -1, -1));
        list_item.Add(new Item(431, "라이터", "재료", 1, -1, -1));
        list_item.Add(new Item(432, "레이저 포인터", "재료", 1, -1, -1));
        list_item.Add(new Item(433, "마패", "재료", 1, -1, -1));
        list_item.Add(new Item(434, "배터리", "재료", 1, -1, -1));
        list_item.Add(new Item(435, "알코올", "재료", 1, -1, -1));
        list_item.Add(new Item(436, "오일", "재료", 1, -1, -1));
        list_item.Add(new Item(437, "옷감", "재료", 1, -1, -1));
        list_item.Add(new Item(438, "원석", "재료", 1, -1, -1));
        list_item.Add(new Item(439, "접착제", "재료", 1, -1, -1));
        list_item.Add(new Item(440, "종이", "재료", 1, -1, -1));
        list_item.Add(new Item(441, "철광석", "재료", 3, -1, -1));
        list_item.Add(new Item(442, "캔", "재료", 1, -1, -1));
        list_item.Add(new Item(443, "화약", "재료", 1, -1, -1));
        list_item.Add(new Item(444, "감시 카메라", "재료", 1, -1, -1));
        list_item.Add(new Item(445, "올가미", "재료", 1, -1, -1));
        list_item.Add(new Item(446, "쥐덫", "재료", 2, -1, -1));
        list_item.Add(new Item(447, "피아노선", "재료", 1, -1, -1));

        #endregion
    }

    void Start()
    {
        RouteAlgorithm();
    }

    void Update()
    {
        Route_in_Sequence();
        RouteInfo();
    }

    #region UI 컨트롤러

    void OnBtnClick(int MapNumber)
    {
        if (!isSelected[MapNumber])
        {
            list_mapNum.Add(MapNumber);
            isSelected[MapNumber] = true;
        }
        else
        {
            list_mapNum.Remove(MapNumber);
            isSelected[MapNumber] = false;
        }

        RouteAlgorithm();
    } // 루트 순서 등록

    void OnMouseEnter(int area)
    {
        List<string> list_s = new List<string>();
        string str = "";

        str = string.Format("[{0}] ", list_mapName[area]);
        list_s.Add(str);

        foreach (int element in list_map[area])
        {
            if (list_itemGuide.Contains(element))
            {
                str = string.Format("({0}) ", list_item[element].name);
                list_s.Add(str);
            }
        }

        str = "";
        foreach (string element in list_s)
        {
            str = string.Format("{0}{1}", str, element);
        }

        txtMapMaterial.text = str;
    } // 지역 재료 표시

    void OnMouseExit()
    {
        txtMapMaterial.text = "";
    }

    void Route_in_Sequence()
    {
        for (int i = 0; i < 16; i++)
        {
            string str1 = "";
            txtMapNum[i].text = list_mapName[i];

            for (int j = 0; j < list_mapNum.Count; j++)
            {
                if (i == list_mapNum[j])
                {
                    str1 = string.Format("{0}", j + 1);
                }
            }

            string str2 = string.Format("{0} {1}", str1, txtMapNum[i].text);
            txtMapNum[i].text = str2;
        }
    } // 루트 순서 표시

    void RouteInfo()
    {
        string str = "";

        foreach (string element in list_RouteInfo)
        {
            str = string.Format("{0}{1}", str, element);
            txtRouteInfo.text = str;
        }
    } // 루트 정보 표시

    #endregion

    public void RouteAlgorithm()
    {
        list_inventory.Clear();
        list_itemGuide.Clear();
        list_RouteInfo.Clear();

        for (int i = 0; i < itemSlot.Length; i++)
        {
            list_item[itemSlot[i]].GetRecipe(); // 목표 아이템 재료 호출
            list_itemGuide.Add(itemSlot[i]);
        }

        list_inventory.Clear();

        #region 시작 무기 생성

        switch (weapon)
        {
            case StartWeapon.DAGGER:
                GetItem(370); // 식칼
                break;

            case StartWeapon.TWOHANDEDSWORD:
                GetItem(371); // 녹슨 검
                break;

            case StartWeapon.AXE:
                GetItem(375); // 손도끼
                break;

            case StartWeapon.DUALSWORDS:
                GetItem(372); // 쌍검
                break;

            case StartWeapon.PISTOL:
                GetItem(392); // 발터 PPK
                break;

            case StartWeapon.ASSAULTRIFLE:
                GetItem(393); // 페도로프 자동소총
                break;

            case StartWeapon.SNIPERRIFLE:
                GetItem(394); // 화승총
                break;

            case StartWeapon.RAPIER:
                GetItem(396); // 바늘
                break;

            case StartWeapon.SPEAR:
                GetItem(376); // 단창
                break;

            case StartWeapon.HAMMER:
                GetItem(373); // 망치
                break;

            case StartWeapon.BAT:
                GetItem(378); // 단봉
                break;

            case StartWeapon.THROW:
                GetItem(386); // 야구공
                break;

            case StartWeapon.SHURIKEN:
                GetItem(387); // 면도칼
                break;

            case StartWeapon.BOW:
                GetItem(390); // ???궁
                break;

            case StartWeapon.CROSSBOW:
                GetItem(391); /// 석궁
                break;

            case StartWeapon.GLOVE:
                GetItem(382); // 목장갑
                break;

            case StartWeapon.TONFA:
                GetItem(379); // 대나무
                break;

            case StartWeapon.GUITAR:
                GetItem(397); // 보급형 기타
                break;

            case StartWeapon.NUNCHAKU:
                GetItem(395); // 쇠사슬
                break;

            case StartWeapon.WHIP:
                GetItem(380); // 채찍
                break;

            case StartWeapon.CAMERA:
                GetItem(398); // 렌즈
                break;
        }

        #endregion

        GetItem(423, 2); // 시작 아이템 물 2개 추가

        for (int i = 0; i < list_mapNum.Count; i++)
        {
            list_RouteInfo.Add(string.Format("[ {0} ]\n", list_mapName[list_mapNum[i]]));

            list_RouteInfo.Add("= 획득 가능 =\n");
            GetMaterials(list_mapNum[i]); //  지역 별 재료 획득

            list_RouteInfo.Add("= 제작 가능 =\n");
            Search(list_mapNum[i]); // 제작할 재료가 모였는지 확인

            list_RouteInfo.Add("\n");
        }

        list_RouteInfo.Add("[ 필요 재료 ]\n");

        for (int i = 0; i < list_itemGuide.Count; i++)
        {
            int count = 0;
            int temp = list_itemGuide[i];

            for (int j = 0; j < list_itemGuide.Count; j++)
            {
                if (list_itemGuide[j] == temp)
                {
                    count++;
                }
            }

            InputRouteInfo(temp, count, 2);
        }
    }

    #region 루트 알고리즘

    void GetItem(int index, int quantity = 1) // 획득 함수
    {
        for (int i = 0; i < quantity; i++)
        {
            list_inventory.Add(index);

            if (list_itemGuide.Contains(index))
            {
                list_itemGuide.Remove(index);
            }
        }
    }

    void GetMaterials(int area) // 재료 수집 함수
    {
        foreach (int materials in list_map[area])
        {
            int count = 0;

            if (list_itemGuide.Contains(materials))
            {
                for (int i = 0; i < list_itemGuide.Count; i++)
                {
                    if (list_itemGuide[i] == materials)
                    {
                        count++;

                        GetItem(materials, list_item[materials].quantity);
                    }
                }

                InputRouteInfo(materials, count * list_item[materials].quantity, 0);

                GetMaterials(area);
            }
        }
    }

    void Search(int area) // 재료 유무 확인 함수
    {
        for (int i = 0; i < list_itemGuide.Count; i++)
        {
            int temp = list_itemGuide[i];
            bool[] isHave = new bool[2] { false, false };

            for (int j = 0; j < list_item[temp].recipe.Length; j++)
            {
                if (list_inventory.Contains(list_item[temp].recipe[j]))
                {
                    isHave[j] = true;
                }
            }

            if (isHave[0] && isHave[1])
            {
                Craft(temp, area);
                i--;
            }
        }
    }

    void Craft(int index, int area) // 제작 함수
    {
        Item tempItem = list_item[index];

        for (int i = 0; i < tempItem.recipe.Length; i++)
        {
            int tempInt = tempItem.recipe[i];
            list_inventory.Remove(tempInt);
        }

        if(list_item[index].type == "장비")
        {
            InputRouteInfo(index, tempItem.quantity, 1);
        }

        GetItem(index, tempItem.quantity); // 아이템 제작
    }

    #endregion

    void InputRouteInfo(int index, int count, int type) // 0 : 분류 없음 1 : 장비 2 : 원재료
    {
        string str = "";
        Item temp = list_item[index];

        switch (type)
        {
            case 0:
                str = string.Format("({0} {1}개)\n", temp.name, count);
                list_RouteInfo.Add(str);
                break;
            case 1:
                str = string.Format("({0})\n", temp.name);
                list_RouteInfo.Add(str);
                break;
            case 2:
                str = string.Format("({0} {1}개)\n", temp.name, count);
                if (temp.recipe[0] == -1 && temp.recipe[1] == -1 && !list_RouteInfo.Contains(str))
                {
                    list_RouteInfo.Add(str);
                }
                break;
        }
    }

    #region 디버그

    public void CheckListMap()
    {
        for (int i = 0; i < 16; i++)
        {
            foreach (int value in list_map[i])
            {
                string str = string.Format("[{0}] {1}번째 아이템: {1}",list_mapName[i]  ,i + 1, value);
                Debug.Log(str);
            }
        }
    }

    public void CheckListItem()
    {
        foreach (Item value in list_item)
        {
            Debug.Log(value.name);
        }
    }

    public void CheckItemGuide()
    {
        for (int i = 0; i < list_itemGuide.Count; i++)
        {
            int temp = list_itemGuide[i];
            string str = string.Format("{0} 번째 목표 아이템: {1}", i, list_item[temp].name);
            Debug.Log(str);
        }
    }

    public void CheckInventory()
    {
        for (int i = 0; i < list_inventory.Count; i++)
        {
            int temp = list_inventory[i];
            string str = string.Format("{0} 번째 인벤토리 아이템: {1}", i, list_item[temp].name);
            Debug.Log(str);
        }
    }

    public void DebugLogClear()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    #endregion
}

public class Item
{
    public int index;
    public string name;
    public string type; // "장비" "재료"
    public int quantity;
    public int[] recipe = new int[2];

    public Item() { }
    public Item(int Index, string Name, string Type, int Quantity, int L_Node, int R_Node)
    {
        index = Index;
        name = Name;
        type = Type;
        quantity = Quantity;
        recipe[0] = L_Node;
        recipe[1] = R_Node;
    }

    public void GetRecipe() // 재료 분해 후 재조립
    {
        for (int i = 0; i < recipe.Length; i++)
        {
            if (recipe[i] != -1)
            {
                Route.list_item[recipe[i]].GetRecipe();
            }
        }

        if (Route.list_inventory.Contains(index))
        {
            Route.list_inventory.Remove(index);
            return; // 인벤토리에 이미 동일 아이템이 있을 경우 재조립 생략
        }

        for (int i = 0; i < quantity - 1; i++)
        {
            Route.list_inventory.Add(index); // 사용하지 않은 여분 아이템 획득
        }

        if(recipe[0] != -1 || recipe[1] != -1)
        {
            for (int i = 0; i < recipe.Length; i++)
            {
                Route.list_itemGuide.Add(recipe[i]); // 실제 사용된 재료 아이템을 리스트에 추가
            }
        }
    }
}