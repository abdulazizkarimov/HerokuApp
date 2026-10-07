// Declarative pipeline for a C# NUnit project.
// Works on both Linux/macOS and Windows agents.

// Run a command with sh on Linux/macOS or bat on Windows
def run(String cmd) {
    if (isUnix()) {
        sh cmd
    } else {
        bat cmd
    }
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
        // Change to your .sln or test .csproj if the repo has more than one
        TEST_TARGET                 = ''
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
                run "dotnet restore ${env.TEST_TARGET}"
            }
        }

        stage('Build') {
            steps {
                run "dotnet build ${env.TEST_TARGET} --configuration Release --no-restore"
            }
        }

        stage('Test') {
            steps {
                run "dotnet test ${env.TEST_TARGET} --configuration Release --no-build --logger \"junit;LogFilePath=${env.WORKSPACE}/TestResults/results.xml\""
            }
        }
    }

    post {
        always {
            // Publish results even when tests fail
            junit testResults: 'TestResults/*.xml', allowEmptyResults: false
        }
    }
}