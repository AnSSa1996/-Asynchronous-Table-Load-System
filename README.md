# -Asynchronous-Table-Load-System
csv파일을 비동기(멀티 쓰레드)를 활용하여 빠르게 로드하는 시스템입니다.


# 특징
* 멀티 쓰레드를 활용해서 데이터를 처리하는 시스템입니다.
* 현재 시스템에서는 간단하게, 데이터를 입력받는 걸 다뤘지만, CSV파일을 파싱하는 것 뿐만아니라 데이터를 처리하는 것이 복잡하다면 __단일 쓰레드보다 훨씬 더 뛰어난 성능을 가질 수 있습니다.__

![image](https://github.com/user-attachments/assets/7449de30-922c-45d5-80e4-de5f622b5f0d)
![image](https://github.com/user-attachments/assets/a63bb0a8-bb9f-4da3-8fd0-2e37e4df0927)



# 사용 방법

1. 아래 그림과 같이 csv파일을 코루틴으로 로드하시면 됩니다. (코루틴은 어떤 Mono르 사용하든 상관 없습니다.)

![image](https://github.com/user-attachments/assets/191709f9-3649-4b1f-94f6-d3827f161db6)
