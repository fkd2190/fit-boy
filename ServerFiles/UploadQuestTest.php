<?php


use PHPUnit\Framework\TestCase;

class UploadQuestTest extends TestCase
{

    private $user_insert_id;

    protected function setUp(): void
    {
        //Add dummy data to database to test
        include('db_connection.php');
        $hash = password_hash('password', PASSWORD_DEFAULT);
        $query = "INSERT INTO users (username, password, email)
                VALUES ('joebloggs', '{$hash}', 'someone@example.com')";
        $conn->query($query);

        //Get dummy user id
        $query = "SELECT LAST_INSERT_ID()";
        $result = $conn->query($query);
        $this->user_insert_id = $result->fetch_assoc()['LAST_INSERT_ID()'];
    }

    public function test_insert_valid_quest()
    {
        //Setup local variables for the register.php script to use.
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['user_id'] = $this->user_insert_id;
        $_POST['start_lat'] = "12.345678";
        $_POST['start_long'] = "12.345678";


        //Run file
        ob_start();
        include('register.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertFalse($response['error']);
    }

    public function test_incorrect_user_id()
    {

    }

    public function test_invalid_coordinates()
    {

    }

    public function test_incorrect_http_post_method()
    {

    }

    protected function tearDown(): void
    {
        include('db_connection.php');
        $query = "DELETE FROM users WHERE user_id = {$this->user_insert_id}";
        $conn->query($query);
    }
}
