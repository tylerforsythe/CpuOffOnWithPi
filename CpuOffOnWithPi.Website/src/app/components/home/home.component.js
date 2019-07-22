import angular from 'angular';
import template from './home.template.html';
import './home.scss';

class homeController {
  constructor($timeout, $http) {
    this.title = 'Switch On/Off';
    console.log('in test controller');

    // $timeout(function () {
    //   console.log('timeout test');
    // }, 1000);

    this.$timeout = $timeout;
    this.$http = $http;
    this.LoadTestServerValue();
  }

  SwitchOn(switchNumber) {
    console.log(`SwitchOn ${  switchNumber}`);
    this.SetSwitchGetNoParam(switchNumber, 'on');
    // this.$timeout(function () {
    //   console.log('timeout test SwitchOn');
    // }, 1000);
  }

  SwitchOff(switchNumber) {
    console.log(`SwitchOff ${  switchNumber}`);
    this.SetSwitchGetNoParam(switchNumber, 'off');
  }

  TransmitSwitch(outletId, outletStatus) {
    this.$http.post('http://localhost:8011/API/Switches/SetSwitch', {
      switchNumber: outletId,
      status: outletStatus,
    }).then((data, status) => {
      console.log(`Outlet toggled! ${data}`);
    });
  }

  TransmitSwitchGet(outletId, outletStatus) {
    this.$http.get('http://localhost:8011/API/Switches/SetSwitchGet', {
      switchNumber: outletId,
      status: outletStatus,
    }).then((data, status) => {
      console.log(`Outlet toggled! ${data}`);
    });
  }

  SetSwitchGetNoParam(outletId, outletStatus) {
    this.$http.post('http://localhost:8011/API/Switches/SetSwitchGetNoParam', {}).then((data, status) => {
      console.log(`Outlet toggled! ${data}`);
    });
  }

  LoadTestServerValue() {
    this.$http.get('http://localhost:8011/API/Switches/Get?id=3', {}).then((data, status) => {
      const str = `Test Value ${data.data}`;
      console.log(str);
      console.log(data);
      this.title = str;
    });
  }
}

homeController.$inject = ['$timeout', '$http'];

angular.module('webapp')
  .component('home', {
    template,
    controller: homeController,
    bindings: {},
  });
