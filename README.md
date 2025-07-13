# VR Table Tennis for BLV Users

## Project Overview



VR Table Tennis is an accessible virtual reality table tennis game specifically designed for Blind and Low-Vision (BLV) users. The game provides an immersive table tennis experience with comprehensive accessibility features including audio guidance, haptic feedback, and spatial audio cues.

### Key Features

- **Audio Guidance System**: Beeping sounds help BLV users locate the paddle in the game environment
- **Haptic Feedback**: Tactile feedback through haptic gloves provides spatial awareness of ball position
- **Spatial Audio**: 3D audio cues for ball movement and collision detection
- **Accessible Controls**: Simplified interaction system optimized for VR accessibility
- **Adaptive Difficulty**: Automatic ball tossing system with configurable parameters

## Project Architecture

The codebase is organized into modular components for maintainability and extensibility:

```
Assets/Scripts/
├── Core/                    # Core game management
│   └── GameManager.cs      # Central game controller and state management
├── Gameplay/               # Core gameplay mechanics
│   ├── Ball.cs            # Ball physics and collision handling
│   └── Tosser.cs          # Automatic ball tossing system
├── Interaction/            # VR interaction systems
│   ├── HandData.cs        # Hand tracking and controller input
│   └── PaddleAttacher.cs  # Paddle pickup and interaction
├── Accessibility/          # Accessibility features for BLV users
│   ├── AudioGuide.cs      # Audio guidance system
│   └── HapticsController.cs # Haptic feedback system
└── UI/                    # User interface components
    └── MenuController.cs   # Menu navigation and scene management
```

### Module Descriptions

#### Core Module
- **GameManager**: Singleton pattern for centralized game state management
- Coordinates between different game systems
- Handles game start, pause, restart, and quit functionality

#### Gameplay Module
- **Ball**: Handles ball physics, collision detection, and audio feedback
- **Tosser**: Manages automatic ball tossing with configurable positions and timing

#### Interaction Module
- **HandData**: Manages VR hand tracking and controller input
- **PaddleAttacher**: Handles paddle pickup mechanics and interaction feedback

#### Accessibility Module
- **AudioGuide**: Provides audio guidance for paddle location
- **HapticsController**: Delivers haptic feedback based on ball position and distance

#### UI Module
- **MenuController**: Handles menu navigation and scene transitions

## Technology Stack

- **Unity 2022.3 LTS**: Game engine
- **XR Interaction Toolkit**: VR interaction framework
- **Bhaptics SDK**: Haptic feedback system
- **Meta XR SDK**: Oculus/Meta VR support
- **Universal Render Pipeline**: Graphics rendering

## Prerequisites

- Unity 2022.3 LTS or later
- Meta Quest 2/3 or compatible VR headset
- Bhaptics haptic gloves (optional but recommended for full experience)
- Meta XR SDK installed

## Installation and Setup

### 1. Clone the Repository
```bash
git clone https://github.com/xability/a11y-vr-tabletennis.git
cd a11y-vr-tabletennis
```

### 2. Open in Unity
1. Launch Unity Hub
2. Open the project folder in Unity 2022.3 LTS
3. Wait for Unity to import all assets and packages

### 3. Configure VR Settings
1. Go to `Edit > Project Settings > XR Plug-in Management`
2. Enable "Oculus" and "OpenXR" plugins
3. Configure your VR headset settings

### 4. Set Up Bhaptics (Optional)
1. Install Bhaptics SDK from the Assets folder
2. Configure haptic glove settings in the HapticsController component
3. Test haptic feedback in the scene

### 5. Build and Deploy
1. Go to `File > Build Settings`
2. Select Android platform
3. Configure build settings for your target device
4. Build and deploy to your VR headset

## How to Run

### Development Mode
1. Open the project in Unity
2. Open the main scene: `Assets/Scenes/game scene.unity`
3. Press Play in the Unity Editor (for testing without VR)
4. Use VR headset for full immersive experience

### Production Build
1. Build the project for Android
2. Install the APK on your Meta Quest device
3. Launch the app from your Quest library

## Configuration

### Audio Settings
- Adjust `activationDistance` in AudioGuide for paddle location sensitivity
- Configure audio sources for different collision types
- Set volume levels for optimal accessibility

### Haptic Settings
- Modify `minDistance` and `maxDistance` in HapticsController
- Adjust `minIntensity` and `maxIntensity` for haptic sensitivity
- Configure motor patterns for different feedback types

### Gameplay Settings
- Tune `throwStrength` in Tosser for ball speed
- Adjust `reload` time for ball frequency
- Configure shot positions for varied gameplay

## Accessibility Features

### Audio Guidance
- Beeping sounds guide users to paddle location
- Distance-based audio intensity
- Automatic audio stop when paddle is picked up

### Haptic Feedback
- Distance-based haptic intensity
- Height-based motor activation patterns
- Real-time feedback for ball position

### Spatial Audio
- 3D audio cues for ball movement
- Collision-specific sound effects
- Volume-based distance indication

## Development Guidelines

### Code Style
- Use C# naming conventions (PascalCase for classes, camelCase for variables)
- Add XML documentation for all public methods
- Follow Unity's component-based architecture

### Modular Design
- Keep scripts focused on single responsibilities
- Use namespaces to organize code modules
- Minimize dependencies between modules

### Testing
- Test accessibility features with BLV users
- Validate haptic feedback patterns
- Ensure audio cues are clear and helpful

## Contributing

### Getting Started
1. Fork the repository
2. Create a feature branch: `git checkout -b feature/new-feature`
3. Make your changes following the coding guidelines
4. Test thoroughly, especially accessibility features
5. Submit a pull request with detailed description

### Contribution Areas
- **Accessibility Improvements**: Enhanced audio/haptic feedback
- **Gameplay Features**: New game modes or mechanics
- **UI/UX**: Better menu systems and user experience
- **Performance**: Optimization for VR performance
- **Documentation**: Improved guides and tutorials

### Testing with BLV Users
- Always test new features with BLV users
- Gather feedback on accessibility effectiveness
- Consider different levels of visual impairment
- Test with various assistive technologies

## Team Members

- **Principal Investigator**: JooYoung Seo
- **Project Lead**: Sanchita S. Kamath
- **Developer**: Dhruv Sethi
- **Co-Designer**: Aziz N. Zeidieh

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For technical support or accessibility questions:
- Create an issue on GitHub
- Contact the development team

## Research Context

This project is part of ongoing research into accessible VR gaming for BLV users. The goal is to create inclusive gaming experiences that provide meaningful engagement and physical activity opportunities for the BLV community.