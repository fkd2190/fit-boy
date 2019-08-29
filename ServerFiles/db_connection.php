<?php
$servername = "fdb20.awardspace.net";
$username = "3135035_fitboy";
$password = "Fit-Boy2019";
$databasename = "3135035_fitboy";
$databaseport = "3306";

// Create connection
$conn = new mysqli($servername, $username, $password, $databasename, $databaseport);

// Check connection
if ($conn->connect_error) {
    $response['error'] = true;
    $response['message'] = "Failed to connect to database.";
    die(json_encode($response));
}