<?php

include_once 'src/AccountApi.php';
include_once 'src/CharityApi.php';
include_once 'src/DonationApi.php';
include_once 'src/SearchApi.php';
include_once 'src/EventApi.php';
include_once 'src/TeamApi.php';
include_once 'src/CountriesApi.php';
include_once 'src/CurrencyApi.php';
include_once 'src/OneSearchApi.php';
include_once 'src/ProjectApi.php';
include_once 'src/SmsApi.php';
include_once 'src/LeaderboardApi.php';
include_once 'src/CampaignApi.php';

class JustGivingClient
{	
	public $ApiKey;
	public $ApiVersion;
	public $Username;
	public $Password;
	public $RootDomain;
	
	public $Page;
	public $Account;
	public $Charity;
	public $Donation;
	public $Search;
	public $Event;
	public $Team;
	public $Countries;
	public $Currency;
	public $OneSearch;
	public $Project;
	public $Sms;
	public $Leaderboard;
	public $Campaign;

	public function __construct($rootDomain, $apiKey, $apiVersion, $username="", $password="")
	{
		$this->RootDomain   	= (string) $rootDomain; 
		$this->ApiKey     		= (string) $apiKey;
		$this->ApiVersion     	= (string) $apiVersion;
		$this->Username     	= (string) $username;
		$this->Password     	= (string) $password;
		$this->curlWrapper		= new CurlWrapper();
		$this->debug			= false;
		
		// Init API clients
		$this->Page				= new PageApi($this);
		$this->Account			= new AccountApi($this);
		$this->Charity			= new CharityApi($this);
		$this->Donation			= new DonationApi($this);
		$this->Search			= new SearchApi($this);
		$this->Event			= new EventApi($this);
		$this->Team			    = new TeamApi($this);
		$this->Countries 		= new CountriesApi($this);
		$this->Currency 		= new CurrencyApi($this);
		$this->OneSearch 		= new OneSearchApi($this);
		$this->Project 			= new ProjectApi($this);
		$this->Sms 				= new SmsApi($this);
		$this->Leaderboard 		= new LeaderboardApi($this);
		$this->Campaign 		= new CampaignApi($this);
	}
}