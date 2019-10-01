<?php
$response['error'] = true;
$response['message'] = "Unknown error";

include "db_connection.php";

if($_SERVER['REQUEST_METHOD'] == "POST"){
    //Local variables
    $username = $_POST['username'];
    $user_id = $_POST['user_id'];
    $quest_name = $_POST['quest_name'];
    $quest_description = $_POST['quest_description'];
	$quest_xp = $_POST['quest_xp'];
    $quest_level = $_POST['quest_level'];
    $start_time = $_POST['start_time'];
    $end_time = $_POST['end_time'];
    $start_lat = $_POST['start_lat'];
    $start_long = $_POST['start_long'];
    $end_lat = $_POST['end_lat'];
    $end_long = $_POST['end_long'];

    //Check if user exists
    $query = "SELECT * FROM users WHERE username = '{$username}'";
    $result = $conn->query($query);
    if($result->num_rows > 0){
        //Add quest to database
        $query = "INSERT INTO quests (quest_name, quest_description, quest_xp, quest_level, start_time, end_time, start_lat, start_long, end_lat, end_long)" .
            "VALUES ('{$quest_name}', '{$quest_description}', {$quest_xp}, {$quest_level}, '{$start_time},', '{$end_time}', {$start_lat}, {$start_long}, {$end_lat}, {$end_long})";

        $conn->query($query);
        //Link quest to user
        $query = "INSERT INTO user_quests (user_id, quest_id) VALUES ({$user_id}, LAST_INSERT_ID())";
        $conn->query($query);
        $response['error'] = false;
        $response['message'] = "Quest successfully added";
    }else{
        $response['error'] = true;
        $response['message'] = "User not found.";
    }
}else{
    $response['error'] = true;
    $response['message'] = "Invalid request method used. Please use POST";
}

echo json_encode($response);
