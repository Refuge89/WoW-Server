<?php

$array = explode("\n", file_get_contents("ItemList.txt"));
$xml = new SimpleXMLElement('<?xml version="1.0" encoding="UTF-8"?><items/>');

foreach ($array as $k => $v) {
	if (substr($v, 0, 1) == "[") {
		$item = $xml->addChild('item');
		$item->addAttribute('id', filter_var($v, FILTER_SANITIZE_NUMBER_INT));
	} else {
        if(strlen($v) >= 2) {
            list($key, $value) = explode('=', $v, 2);
            $item->addChild(strtolower($key), trim($value));
        }
	}
}

//Format XML to save indented tree rather than one line
$dom = new DOMDocument('1.0');
$dom->preserveWhiteSpace = false;
$dom->formatOutput = true;
$dom->loadXML($xml->asXML());
$dom->save('items.xml');