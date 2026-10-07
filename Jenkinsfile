// Declarative pipeline for a C# NUnit project.
// Works on both Linux/macOS and Windows agents.

// Run a command with sh on Linux/macOS or bat on Windows
def runCmd(String cmd) {
    if (isUnix()) {
        sh cmd
    } else {
        bat cmd
    }
}

// Path to your .sln or test .csproj, relative to the repo root.
// Leave empty ('') if the repo root contains exactly one .sln or .csproj.
// Example: 'HerokuApp.Tests/HerokuApp.Tests.csproj'
def testTarget() {
    return ''
}

pipeline {
    agent any

    options {
        timestamps()
        timeout(time: 30, unit: 'MINUTES')
        buildDiscarder(logRotator(numToKeepStr: '20'))
    }

    // Optional: poll GitHub every ~5 minutes.
    // Remove this if you use a GitHub webhook instead.
    triggers {
        pollSCM('H/5 * * * *')
    }

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_NOLOGO               = '1'
    }

    stages {
        stage('Checkout') {
            steps {
                // Uses the repository and branch configured in the job
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                runCmd "dotnet restore ${testTarget()}"
            }
        }

        stage('Build') {
            steps {
                runCmd "dotnet build ${testTarget()} --configuration Release --no-restore"
            }
        }

        stage('Test') {
            steps {
                runCmd "dotnet test ${testTarget()} --configuration Release --no-build --logger \"junit;LogFilePath=${env.WORKSPACE}/TestResults/results.xml\""
            }
        }
    }

    post {
        always {
            // Publish results even when tests fail.
            // allowEmptyResults avoids a second, confusing error if the build
            // failed before any tests ran.
            junit testResults: 'TestResults/*.xml', allowEmptyResults: true
        }
    }
}