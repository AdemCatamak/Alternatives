#!/usr/bin/env groovy
#!jenkins.branch.WorkspaceLocatorImpl.PATH_MAX=1
branchName = env.BRANCH_NAME
buildId = env.BUILD_ID;

node('master'){
    stage('Clean and Checkout'){
    try { checkout scm } catch(caughtError) { deleteDir(); checkout scm }
    }
    stage('Run') {
      posh "./build.ps1 -experimental -branch=\"${branchName}\" -buildNumber=\"${buildId}\""
    } 
}

def posh(cmd) {
  bat 'powershell.exe -NoProfile -ExecutionPolicy Bypass -Command "& ' + cmd + '"'
}
