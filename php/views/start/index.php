<body>
<?=isset($message) ? $message :''?>

<?php
/**
 * Created by JetBrains PhpStorm.
 * User: jferrara
 * Date: 5/3/12
 * Time: 2:21 PM
 * To change this template use File | Settings | File Templates.
 */

echo form_open( 'start/testx' );

$data =array(
    'name'        => 'newsletter',
    'id'          => 'newsletter',
    'value'       => 'check1',
    'checked'     => TRUE,
    'style'       => 'margin:10px',
);

echo form_checkbox($data);br();
$data =array(
    'name'        => 'other',
    'id'          => 'other',
    'value'       => 'check2',
    'checked'     => TRUE,
    'style'       => 'margin:10px',
);
echo form_checkbox($data);
echo br();

$data =array(
    'name'        => 'check1',
    'id'          => 'check1',
    'value'       => 'accept',
    'checked'     => false,
    'style'       => 'margin:10px',
    'class'       => 'inline'
);

echo "Two  ". form_radio($data);

$data =array(
    'name'        => 'check1',
    'id'          => 'check2',
    'value'       => 'accept2',
    'checked'     => false,
    'style'       => 'margin:10px',
    'class'       => 'inline'
);

echo 'One  '.form_radio($data);br();


echo form_submit(array('type'=>'submit', 'name'=>'submit','class'=>'btn-primary btn-small'),


    'test click here!');br();
form_close();

?>
<br>
<i class='icon-ok'> </i>Hello
<br>
this is the start page

<form class="" action="http://localhost:8080/intern/index.php/start/testx" method="post" accept-charset="utf-8">
    <fieldset class="control-group error">

        <label>Label name</label>
        <input type="text" id="testtext" class="span3" placeholder='Type something'/>
        <span class=" help-inline " for="testtext">Error for input</span><br/>

        <label>Label name</label>
        <input type="text" id="mytext" class="span3" placeholder='Type something'/>
        <span class=" help-inline " for="mytext">Error for input</span><br/>

        <label>Select all that apply: </label>

       <label class="checkbox inline paddingRight"><input name='ck1' id='ck1' value='one' type="checkbox"/>Check me out</label>
        <label class="checkbox inline paddingRight"><input name='ck2' id='ck2' value='two' type="checkbox"> Check me out   </label>
        <label class="checkbox inline paddingRight"><input name='ck3' id='ck3' value='three' type="checkbox"> Check me out</label>
        <label class="checkbox inline paddingRight"><input name='ck4' id='ck4' value='four' type="checkbox"> Check me out</label>
        <label class="checkbox inline paddingRight"><input name='ck5' id='ck5' value='five'  type="checkbox"> Check me out</label>

        <br>
        <label>Select one: </label>
        <label class="radio inline paddingRight"><input name='radiogroup' id='r1' value='rone' class="radio"  type="radio"> Check me out</label>
        <label class="radio inline paddingRight"><input name='radiogroup' id='r2' value='rtwo' class="radio" type="radio"> Check me out</label>
        <label class="radio inline paddingRight"><input name='radiogroup' id='r3' value='rthree' class="radio" type="radio"> Check me out</label>
        <label class="radio inline paddingRight"><input name='radiogroup' id='r4' value='rfour' type="radio"> Check me out</label>
        <label class="radio inline paddingRight"><input name='radiogroup' id='r5' value='rfive' type="radio"> Check me out</label>




    </fieldset>
    <button type="submit" class="btn">Submit</button>
</form>




</body>