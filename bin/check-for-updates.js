const fs = require('fs');
const path = require('path');
const axios = require('axios');
const xml2js = require('xml2js');

// Read the GitHub token from the environment variable
const GITHUB_TOKEN = process.env.GITHUB_TOKEN;

if (!GITHUB_TOKEN) {
  console.error('Error: GitHub token is not set in the environment variables.');
  process.exit(1);
}

const configFilePath = path.join(__dirname, '../Editor/Android/AdGemDependencies.xml');

async function checkAdGemSdkUpdates() {
  // Read the current version from AdGemDependencies.xml
  const xmlContent = fs.readFileSync(configFilePath, 'utf8');
  const parser = new xml2js.Parser();
  const xml = await parser.parseStringPromise(xmlContent);
  const currentVersion = xml.dependencies.androidPackages[0].androidPackage[0].$.spec.split(':').pop();

  // Get the latest version from the AdGem GitHub releases
  const response = await axios.get('https://api.github.com/repos/AdGem/Android-SDK/releases/latest', {
    headers: {
      'Accept': 'application/vnd.github+json',
      'Authorization': `Bearer ${GITHUB_TOKEN}`,
      'X-GitHub-Api-Version': '2022-11-28'
    }
  });

  let latestVersion = response.data.tag_name;
  latestVersion = latestVersion.replace(/^v/, ''); // Remove leading 'v'

  if (latestVersion !== currentVersion) {
    // Update the AdGemDependencies.xml with the new version
    xml.dependencies.androidPackages[0].androidPackage[0].$.spec = `com.adgem:adgem-android:${latestVersion}`;
    const builder = new xml2js.Builder();
    const updatedXmlContent = builder.buildObject(xml);
    fs.writeFileSync(configFilePath, updatedXmlContent);
  }

  console.log(latestVersion);
}

checkAdGemSdkUpdates();