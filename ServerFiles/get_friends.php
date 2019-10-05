<?php
$response['error'] = true;
$response['message'] = "Unknown Error";
$response['friends'] = null;
include "db_connection.php";

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $user_id = $_POST['user_id'];
    $query = "SELECT username, xp, level FROM users u, friends f where u.user_id = f.friend_id AND f.user_id = {$user_id}";
    $result = $conn->query($query);
    if(!$conn->error) {
        $response['error'] = false;
        $response['message'] = "Retrieved Friends";
        if ($result->num_rows > 0) {
            for ($i = 0; $i < $result->num_rows; $i++) {
                $response['friends'][$i] = $result->fetch_assoc();
            }
        }
    }else{
        $response['error'] = true;
        $response['message'] = "Error with database: " . $conn->error;
    }
}else{
    $response['error'] = true;
    $response['message'] = "Invalid request method used. Please use POST";
}
echo json_encode($response);
