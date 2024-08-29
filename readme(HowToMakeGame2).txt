HOW TO MAKE GAME 2: HƯỚNG DẪN TOÀN BỘ

I. Import asset vào dự án
- Chúng ta không thể thiết kế và vẽ 1 vật thể 3D trong Unity, để làm được điều này thì phải sử dụng phần mềm về đồ họa (vd: blender) rồi Import vào dự án Unity
- Vì thế sử dụng asset có sẵn, Import vào dự án để lấy các vật thể (bếp, bàn cắt đồ ăn, bàn để đồ, ...), các material (màu) và nhiều thứ khác
- Link: https://unitycodemonkey.com/kitchenchaoscourse.php

II. Post Processing
- Có tác dụng giống filter trong máy ảnh
- Dùng Global Volume để dùng Post Processing
- Nhấn vào Global Volume, xuất hiện Inspector bên phải màn hình, nhấn Add Component, thêm cách Component như Color Adjustment, Vignette ... rồi điều chỉnh theo ý thích

III. Thêm vật thể
- Góc trái dưới có Hierarchy, đây là tab chứa các vật thể, chúng ta có thể thêm, xóa, sửa các vật thể

B1: Nhấn chuột phải và nhấn 3D Object để tạo vật thể 3D, trong đây chứa các vật thể cơ bản, nhấn vào Plane để tạo cái sàn
B2: Điều chỉnh điều chỉnh lại tọa độ xyz là 000, độ lớn của sản ở Inspector bên phải màn hình
B3: Để thêm Plane này nhìn giống sàn nhà hơn thì vào _Assets -> Materials, kiếm Floor rồi kéo thả vào Plane
- Chúng ta có thể thay thuộc tính của Materials
B4: Tạo 1 vật thể trống, điều chỉnh lại tọa độ xyz là 000, vào _Assets -> PrefabVisuals -> kiếm PlayerVisual rồi thả vào vật thể trống đó, đổi tên vật thể trống là Player

*Lưu ý: Chúng ta nên tạo vật thể trống rồi thêm Visual vào, vật thể trống đóng vai trò về Logic và con là PlayerVisual đóng vai trò Visual (hình ảnh), làm vậy sẽ tạo sự rõ ràng hơn và dễ quản lý hơn về sau


IV. Điều khiển Player
B1: Tạo 1 file Script C#, đặt tên Player
B2: Kéo file này vào Player (không phải là PlayerVisual), nhấn 2 lần vào file đó để vào VisualCode
B3: Ra ngoài Unity, nhấn vào Window ở trên, vào Package Manager, đổi Packages: In Project thành Registry
B4: Kéo xuống và tìm Input Action, tải về

- Input Action: là 1 package xử lý đầu vào từ người chơi (vd: nhấn phím, di chuột...)

B5: Tạo 1 Input Action bằng cách nhấn dấu + ở gần chữ Project và kéo xuống 1 tí, đặt tên PlayerInputAction
B6: Tạo mới Action Maps, đặt tên là Player, tạo mới 1 Action, đặt tên là Move, chọn Action Type là Value, Control Type là Vector3 (trong Unity 3D có tọa độ xyz nên dùng Vector3 để tính toán hướng đi)
B7: Thêm 4 Binding, thêm phím WSAD (nút để di chuyển) bên Binding Properties
B8: 

- Sau khi Save Asset, nó tự động tạo 1 C# Script có tên đã đặt (PlayerInputAction), trong đây là class xử lý đầu vào của người chơi

B8: Tạo 1 C# Script đặt tên là GameInput, mở file
B9: Hãy viết như sau

public static GameInput Instance {get; private set;} //gọi là mô hình singleton

//* Lưu ý: sử dụng singleton khi class đó là class duy nhất (không được phép có bản sao) và thường xuyên được tham chiếu (sử dụng), ưu điểm là hạn chế sự khởi tạo class này bên class khác
//vd: 

PlayerInputAction playerInputAction //Gọi lớp PlayerInputAction để có thể lấy giá trị Vector3 khi người chơi nhấn WSAD

private void Awake(){ //đây là hàm sẽ được gọi ngay khi game bắt đầu, đây là nơi thích hợp để khởi tạo giá trị và thiết lập tham chiếu
	Instance = this;
	
	playerInputAction = new playerInputAction();

	playerInputAction.Player.Enable(); //cho phép playerInputAction.Player hoạt động
}

public Vector3 GetPlayerMovementInput(){
	Vector3 inputVector = playerInputAction.Player.Move.ReadValue<Vector3>(); //lấy giá trị Vector3

	inputVector = inputVector.normalized; //có tác dụng là giúp nhân vật được điều khiển không di chyển quá nhanh khi đi hướng chéo (liên quan toán học)

	return inputVector;
}

B10: Tạo 1 C# Script đặt tên là Player, mở file
B10: Hãy viết như sau