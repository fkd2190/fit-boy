<?php


use PHPUnit\Framework\TestCase;

class AuthenticateUserTests extends TestCase
{
    protected function setUp(): void
    {
        //Add dummy data to database to test
        include('db_connection.php');
        $hash = password_hash('password', PASSWORD_DEFAULT);
        $query = "INSERT INTO users (username, password, email)
                VALUES ('joebloggs', '{$hash}', 'someone@example.com')";
        $conn->query($query);

    }

    public function test_authenticate_valid_user()
    {
        //Set up local variables for authenticate_user.php to use
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "joebloggs";
        $_POST['password'] = "password";

        //Run file
        ob_start();
        include('authenticate_user.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertFalse($response['error']);
        $this->assertNotNull($response['user']);
    }

    public function test_invalid_password()
    {
        //Set up local variables for authenticate_user.php to use
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "joebloggs";
        $_POST['password'] = "invalidPassword";

        //Run file
        ob_start();
        include('authenticate_user.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertTrue($response['error']);
        $this->assertNull($response['user']);
    }

    public function test_invalid_username()
    {
        //Set up local variables for authenticate_user.php to use
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "invalidUsername";
        $_POST['password'] = "password";

        //Run file
        ob_start();
        include('authenticate_user.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertTrue($response['error']);
        $this->assertNull($response['user']);
    }

    public function test_invalid_http_method()
    {
        //Setup local variables for the register.php script to use.
        $_SERVER["REQUEST_METHOD"] = "GET";
        $_POST['username'] = "joebloggs";
        $_POST['password'] = "password";

        //Run file
        ob_start();
        include('authenticate_user.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertTrue($response['error']);
        $this->assertNull($response['user']);
    }

    protected function tearDown(): void
    {
        include('db_connection.php');
        $query = "DELETE FROM users WHERE username = 'joebloggs'";
        $conn->query($query);
    }
}
