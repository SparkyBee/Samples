<?php
/**
 * Created by JetBrains PhpStorm.
 * User: Barrett
 * Date: 4/29/12
 * Time: 7:09 PM
 * To change this template use File | Settings | File Templates.
 */
class Dashboard extends MY_Controller
{
    function __construct(){

        parent::__construct();

        if(!$this->user) redirect('auth/login');
    }
    function index(){
        die('dashboard index');
    }
}
