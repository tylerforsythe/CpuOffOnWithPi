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
    this.LoadCpusFromServer();
  }

  SwitchOn(switchNumber) {
    console.log(`SwitchOn ${switchNumber}`);
    this.TransmitSwitch(switchNumber, 'on');
    // this.$timeout(function () {
    //   console.log('timeout test SwitchOn');
    // }, 1000);
  }

  SwitchOff(switchNumber) {
    console.log(`SwitchOff ${switchNumber}`);
    this.TransmitSwitch(switchNumber, 'off');
  }

  TransmitSwitch(outletId, outletStatus) {
    this.$http.post('http://localhost:8011/API/Switches/SetSwitch', {
      switchNumber: outletId,
      status: outletStatus,
    }).then((data, status) => {
      console.log(`Outlet toggled! ${data.data}`);
    });
  }

  LoadCpusFromServer() {
    this.$http.post('http://localhost:8011/API/Cpu/GetCpus', {}).then((data, status) => {
      const str = `Test Value ${data.data}`;
      console.log(str);
      console.log(data);
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
