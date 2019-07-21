import angular from 'angular';
import template from './home.template.html';
import './home.scss';

class homeController {
  constructor() {
    this.title = 'Switch On/Off';
  }
}

homeController.$inject = [];

angular.module('webapp')
  .component('home', {
    template,
    controller: homeController,
    bindings: {},
  });
