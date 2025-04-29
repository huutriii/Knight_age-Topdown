# KnightAge TopDown - Thư Mục Script

Thư mục này chứa tất cả các script cho dự án game KnightAge TopDown. Các script được tổ chức thành nhiều thư mục con dựa trên chức năng của chúng.

## Cấu Trúc Thư Mục

### Hệ Thống Cốt Lõi

- `GAME.cs` - Chứa các hằng số và định danh cốt lõi của game
- `STATE.cs` - Định nghĩa các trạng thái nhân vật (đứng yên, chạy, tấn công, v.v.)
- `SOUND.cs` - Xử lý các chức năng liên quan đến âm thanh

### Thành Phần Chính Của Game

- `Player/` - Script và hành vi của nhân vật người chơi
- `Monster/` - AI và hành vi của kẻ địch
- `Boss/` - Script cho boss đặc biệt
- `NPC/` - Tương tác với nhân vật không phải người chơi
- `Tower/` - Chức năng liên quan đến tháp
- `Area/` - Quản lý khu vực/level

### Cơ Chế Game

- `Skill/` - Hệ thống kỹ năng và khả năng
- `Inventory/` - Hệ thống quản lý kho đồ
- `Class/` - Định nghĩa lớp nhân vật và khả năng

### Hiệu Ứng Hình Ảnh và Âm Thanh

- `Efx/` - Hiệu ứng hình ảnh
- `HitEffect/` - Hiệu ứng đòn đánh/thiệt hại
- `Audio/` - Hiệu ứng âm thanh và nhạc nền
- `Camera/` - Điều khiển và hành vi của camera

### Giao Diện Người Dùng

- `UI/` - Các thành phần giao diện người dùng và HUD

### Khu Vực Đặc Biệt

- `Wolf village/` - Script đặc thù cho làng sói

## Trạng Thái Game

Game sử dụng các trạng thái nhân vật sau:

- `idle` - Trạng thái đứng yên mặc định
- `run` - Di chuyển chạy
- `walk` - Di chuyển đi bộ
- `attack` - Trạng thái tấn công
- `hurt` - Trạng thái bị thương
- `died` - Trạng thái chết
- `attack_reverse` - Trạng thái tấn công đặc biệt

## Điều Khiển

- Di chuyển ngang: Trục "Horizontal"
- Di chuyển dọc: Trục "Vertical"
- Kích hoạt kỹ năng: Phím "Skill"
- Kỹ năng đặc biệt (Sấm sét): Phím "Thunder"

## Ghi Chú Phát Triển

- Đây là dự án game cá nhân góc nhìn từ trên xuống
- Script được tổ chức theo chức năng để dễ bảo trì
- Các hằng số game cốt lõi được tập trung trong GAME.cs
- Quản lý trạng thái được xử lý thông qua STATE.cs
