<!DOCTYPE html>
<html>

<head>
    <meta charset="UTF-8">
    <style>
        .form-group {
            margin: 10px 0;
        }
        input {
            margin: 5px;
            padding: 5px;
        }
        .result {
            margin: 10px;
            padding: 10px;
            background-color: #f0f0f0;
            border-radius: 5px;
        }
    </style>
</head>

<body>
    <!-- Form nhập số lượng -->
    <form method="post" action="">
        <div class="form-group">
            <label>Nhập số lượng textbox: </label>
            <input type="number" name="quantity" min="1" required>
            <input type="submit" name="generate" value="Tạo">
        </div>
    </form>

    <!-- Form chứa các textbox được tạo -->
    <?php
    if(isset($_POST['generate'])) {
        $quantity = (int)$_POST['quantity'];
        if($quantity > 0) {
            echo "<form method='post' action=''>";
            echo "<div class='form-group'>";
            for($i = 1; $i <= $quantity; $i++) {
                echo "<input type='text' name='number$i' placeholder='Số $i' type='number'>";
            }
            echo "<input type='submit' name='calculate' value='Tính'>";
            echo "</div>";
            echo "</form>";
        }
    }

    // Xử lý khi form thứ hai được submit
    if(isset($_POST['calculate'])) {
        $sumEven = 0;
        $evenNumbers = array();
        
        foreach($_POST as $key => $value) {
            if(strpos($key, 'number') === 0 && $value != '') {
                $num = (int)$value;
                if($num % 2 == 0) {
                    $sumEven += $num;
                    $evenNumbers[] = $num;
                }
            }
        }
        
        echo "<div class='result'>";
        echo "Các số chẵn: " . implode(", ", $evenNumbers) . "<br>";
        echo "Tổng các số chẵn: " . $sumEven;
        echo "</div>";
    }
    ?>

</body>

</html>